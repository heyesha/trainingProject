namespace TrainingProject.Domain.Enum
{
    public enum ErrorCodes
    {
        ReportsNotFound = 0,
        ReportNotFound = 1,
        ReportAlreadyExists = 2,

        UserNotFound = 11,
        UserAlreadyExists = 12,
        UserUnauthorizedAccessException = 13,

        PasswordNotEqualsPasswordConfirm = 21,
        PasswordIsWrong = 22,

        InternalServerError = 10
    }
}
