namespace CardTrader.DataAccess.Exceptions;

public class EntityNotFoundException(string message = "Entity not found")
    : Exception(message);

public class AccessDeniedException(string message = "Access denied")
    : Exception(message);

public class SaveFailedException(string message = "Save failed")
    : Exception(message);
