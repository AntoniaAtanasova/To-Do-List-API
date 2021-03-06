namespace ToDoList.Common
{
    public class Constants
    {
        public const string BulgariaCode = "BG";
        public const string HolidayApiUrl = "/api/v2/PublicHolidays/{0}/{1}";
        public const string HolidayApiClientName = "PublicHolidaysApi";

        public const int BadRequest = 400;
        public const int NotFound = 404;
        public const int InternalServerError = 500;

        public const string UserNameNotAvailable = "Username {0} not available!";
        public const string UserNotFound = "User not found!";
        public const string UserRoleNotValid = "User role not valid!";
        public const string EmailAlreadyInUse = "The email address you want to register is already taken!";
        public const string ListTitleTaken = "List title {0} not available!";
        public const string NotCreator = "You are not the creator of list {0} and you cannot execute this action!";
        public const string ListNotFound = "To-do list not found!";
        public const string TaskTitleTaken = "Task title {0} not available!";
        public const string TaskAlreadyAssigned = "Task {0} is already assigned!";
        public const string TaskNotFound = "Task not found!";
        public const string NotTaskCreatorOrAssignedTo = "You are not the creator of task {0} nor it is assigned to you!";
    }
}
