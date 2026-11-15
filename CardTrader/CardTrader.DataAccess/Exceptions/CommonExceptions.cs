namespace CardTrader.DataAccess.Exceptions;

public class EntityNotFoundException(string message = "Entity not found")
    : Exception(message);
