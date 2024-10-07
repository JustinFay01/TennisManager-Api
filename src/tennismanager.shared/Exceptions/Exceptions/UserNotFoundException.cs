using tennismanager.shared.Exceptions.Exceptions.Abstract;

namespace tennismanager.shared.Exceptions.Exceptions;

public class UserNotFoundException() : EntityNotFoundException("User not found");