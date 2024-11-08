import asyncio
import aiohttp
import random
import argparse
from dataclasses import dataclass, field
from typing import Optional, List
import json
from datetime import datetime, timedelta
import requests
from tqdm import tqdm
from colorama import Fore, Style, init
from pytz import timezone


# Argument parser
parser = argparse.ArgumentParser(description='Populate the database with fake data.')
parser.add_argument('--num_customers', '-nc', type=int, default=10, help='Number of customers to create')
parser.add_argument('--num_sessions', '-ns', type=int, default=10, help='Number of sessions to create')
parser.add_argument('--use_https', '-u', action='store_true', default=False, help='Use HTTPS instead of HTTP')
parser.add_argument('--verbose', '-v', action='store_true', default=False, help='Print debug information')
args = parser.parse_args()

init(autoreset=True)

# Constants
if args.use_https:
    base_url = "https://localhost:7168/api"
else:
    base_url = "http://localhost:5233/api"

users_url = f"{base_url}/users"
session_url = f"{base_url}/sessions"
headers = {
    "Content-Type": "application/json"
}


# Models for the data
@dataclass
class UserRequest:
    type: str # "customer" or "coach"
    firstName: str
    lastName: str
    email: Optional[str] = None
    phoneNumber: Optional[str] = None
    picture: Optional[str] = None
    nickname: Optional[str] = None
    hourlyRate: Optional[float] = None

    def to_json(self):
        return json.dumps(self, default=lambda o: o.__dict__, indent=4)
    
    @staticmethod
    def random_instance():
        first_name = random.choice(["Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Heidi", "Ivan", "Judy"])
        last_name = random.choice(["Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor"])
        return UserRequest(
            type=random.choice(["customer", "coach"]),
            firstName=first_name,
            lastName=last_name,
            email=f"{first_name.lower()}.{last_name.lower()}@example.com",
            phoneNumber=f"+1{random.randint(1000000000, 9999999999)}",
            picture="N/A",
            nickname=random.choice([None, first_name[0] + last_name[0]]),
            hourlyRate=round(random.uniform(10.0, 100.0), 2) if random.choice([True, False]) else None
        )

@dataclass
class SessionIntervalRequest:
    recurringStartDate: Optional[str] = None
    repeatInterval: Optional[int] = None

    @staticmethod
    def random_instance():
        start_date = datetime.now() + timedelta(days=random.randint(0, 30))
        return SessionIntervalRequest(
            recurringStartDate=start_date.isoformat(),
            repeatInterval=random.randint(86400, 86400*7)
        )

@dataclass
class SessionMetaRequest:
    recurring: Optional[bool] = None
    startDate: Optional[str] = None
    endDate: Optional[str] = None
    sessionIntervals: List[SessionIntervalRequest] = field(default_factory=list)

    @staticmethod
    def random_instance():
        start_date = (datetime.now() + timedelta(days=random.randint(0, 30))).astimezone(timezone.utc)
        end_date = (start_date + timedelta(days=random.randint(30, 365))).astimezone(timezone.utc)
        recurring = random.choice([True, False])
        return SessionMetaRequest(
            recurring=recurring,
            startDate=start_date.isoformat(),
            endDate=end_date.isoformat(),
            sessionIntervals=[SessionIntervalRequest.random_instance() for _ in range(random.randint(1, 3))] if recurring else [])

@dataclass
class SessionRequest:
    type: str
    name: str
    price: float
    duration: int
    capacity: int
    description: Optional[str] = None
    coachId: Optional[str] = None
    sessionMeta: SessionMetaRequest = field(default_factory=SessionMetaRequest)

    def to_json(self):
        return json.dumps(self, default=lambda o: o.__dict__, indent=4)

    @staticmethod
    def random_instance():
        return SessionRequest(
            type="Event",
            name=random.choice(["Tennis", "Wimbledon", "US Open", "French Open", "Australian Open"]),
            price=round(random.uniform(10.0, 100.0), 2),
            duration=random.choice([30, 45, 60, 90]),
            capacity=random.randint(5, 20),
            description=random.choice([None, "A relaxing session", "An intense workout"]),
            coachId=None,
            sessionMeta=SessionMetaRequest.random_instance()
        )



# Main functions
def wait_for_input():
    validInput = False
    while not validInput:
        response = input(f"{Fore.YELLOW}Would you like to continue? [y/n] {Style.RESET_ALL}")
        if response.lower() == "y":
            validInput = True
        elif response.lower() == "n":
            validInput = True
            print(f"{Fore.RED}User chose to stop the script{Style.RESET_ALL}")
            exit()
        else:
            print(f"{Fore.RED}Invalid input. Please enter 'y' or 'n'.{Style.RESET_ALL}")


def create_customer():
    user = UserRequest.random_instance()
    try:
        response = requests.post(users_url, headers=headers, data=user.to_json())
    except Exception as e:
        print(f"{Fore.RED}Error creating customer: {e}{Style.RESET_ALL}")
        wait_for_input()
    
    # Print response status code and content for debugging
    if args.verbose:
        print(f"Response status code: {response.status_code}")
        print(f"Response content: {response.content}")
            

def create_session():
    session = SessionRequest.random_instance()
    try:
        response = requests.post(session_url, headers=headers, data=session.to_json())
        if not (response.status_code <= 299  and response.status_code >= 200):
            print(f"{Fore.RED}Error creating session: {response.content}{Style.RESET_ALL}")
            wait_for_input()
    except Exception as e:
        print(f"{Fore.RED}Error creating session: {e}{Style.RESET_ALL}")
        wait_for_input()

    # Print response status code and content for debugging
    if args.verbose:
        print(f"Response status code: {response.status_code}")
        print(f"Response content: {response.content}")
    

total_requests = args.num_customers + args.num_sessions
for _ in tqdm(range(total_requests), desc="Creating data"):
    for _ in tqdm(range(args.num_customers), desc="Creating customers", leave=False):
        create_customer()
    for _ in tqdm(range(args.num_sessions), desc="Creating sessions", leave=False):
        create_session()

async def main():
    async with aiohttp.ClientSession() as session:
        tasks = []
        for _ in range(args.num_customers):
            tasks.append(create_customer(session))
        for _ in range(args.num_sessions):
            tasks.append(create_session(session))
        await asyncio.gather(*tasks)

if __name__ == "__main__":
    asyncio.run(main())
