using tennismanager.service.Exceptions.Abstract;

namespace tennismanager.service.Exceptions;

public class SessionNotFoundException() : EntityNotFoundException("Session not found");