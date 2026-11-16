namespace CardTrader.DataAccess.Exceptions;

// Authentication failures

public class InvalidCredentialsException(string message = "Email or password is invalid")
    : Exception(message);

public class UserLockedOutException(string message = "Too many failed attempts. User is locked out")
    : Exception(message);

public class InvalidRefreshTokenException(string message = "Invalid refresh token")
    : Exception(message);

// Conflicts

public class DuplicateUserException(string message = "User already exists")
    : Exception(message);
