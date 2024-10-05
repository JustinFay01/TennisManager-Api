using tennismanager.shared.Exceptions.Exceptions.Abstract;

namespace tennismanager.shared.Exceptions.Exceptions;

public class SessionNotFoundException() : EntityNotFoundException("Session not found");