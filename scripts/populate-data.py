# Script serves to quickly add fake data to the docker db using the running endpoints
import requests
import random


# Constants
base_url = "http://localhost:7168/api"
customer_url = f"{base_url}/customers"
create_customer_url = f"{customer_url}/create"
session_url = f"{base_url}/sessions"
create_session_url = f"{session_url}/create"
headers = {
    "Content-Type": "application/json"
}


# SessionRequest = {
#   Type: Event, Tennis Drill, ...
#   Name: String,
#   Description: String,
#   Price: Decimal,
#   Capacity: Int,
#   SessionMeta = {
#     Recurring: Boolean,
#     StartDate: Date,
#     EndDate: Date,
#    }
#
# }
def create_random_session(index: int):
    # Create a random session
    session = {
        "Type": "Event",
        "Name": f"Tennis Drills-{index}",
        "Description": "Tennis drills for beginners",
        "Price": random.randint(10, 100),
        "Capacity": random.randint(5, 20),
        "SessionMeta": {
            "Recurring": False,
            "StartDate": "2021-10-01",
            "EndDate": "2021-10-30"
        }
    }
    return session


# MAIN SCRIPT
# Create 10 Sessions
for i in range(10):
    session = create_random_session(i)
    response = requests.post(create_session_url, json=session, headers=headers, verify=False)