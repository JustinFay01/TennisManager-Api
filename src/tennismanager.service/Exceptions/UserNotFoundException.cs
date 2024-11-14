using tennismanager.service.Exceptions.Abstract;

namespace tennismanager.service.Exceptions;

public class UserNotFoundException() : EntityNotFoundException("User not found");