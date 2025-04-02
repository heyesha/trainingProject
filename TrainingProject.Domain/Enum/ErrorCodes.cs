﻿namespace TrainingProject.Domain.Enum
{
    public enum ErrorCodes
    {
        ReportsNotFound = 0,
        ReportNotFound = 1,
        ReportAlreadyExists = 2,

        UserNotFound = 11,
        UserAlreadyExists = 12,

        PasswordNotEqualsPasswordConfirm = 21,

        InternalServerError = 10
    }
}
