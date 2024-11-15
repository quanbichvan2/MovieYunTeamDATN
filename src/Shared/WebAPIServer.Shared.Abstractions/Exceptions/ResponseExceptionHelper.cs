using FluentValidation.Results;
using System.ComponentModel;
using System.Reflection;
using WebAPIServer.Shared.Abstractions.Enums;

namespace WebAPIServer.Shared.Abstractions.Exceptions
{
    public static class ResponseExceptionHelper
    {
        /// <summary>
        /// Hiển thị lỗi khi validate thất bại
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="validationErrors"></param>
        /// <param name="localization"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ResponseException ErrorResponse<TEntity>(
           ErrorCode errorCode,
           List<ValidationFailure> validationErrors
            )
        {
            string entityName = typeof(TEntity).Name;
            string message = string.IsNullOrEmpty(entityName) ? errorCode.GetDescription() : errorCode.GetDescription(entityName);
            var errors = new Dictionary<string, List<object>>();
            foreach (var error in validationErrors)
            {
                if (!errors.ContainsKey(error.PropertyName))
                {
                    errors[error.PropertyName] = new List<object>();
                }
                errors[error.PropertyName].Add(error.ErrorMessage);
            }
            return new ResponseException
            {
                Message = message,
                Errors = errors,
                IsSuccess = false
            };
        }

        /// <summary>
        /// Hiển thị lỗi khi ...
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static ResponseException ErrorResponse<TEntity>(
            ErrorCode errorCode
            )
        {
            string entityName = typeof(TEntity).Name;
            string message = string.IsNullOrEmpty(entityName) ? errorCode.GetDescription() : errorCode.GetDescription(entityName);
            return new ResponseException
            {
                Message = message,
                IsSuccess = false
            };
        }
        public static ResponseException ErrorResponse<TEntity>(
            ErrorCode errorCode,
            string errorMessage
            )
        {
            string entityName = typeof(TEntity).Name;
            string message = string.IsNullOrEmpty(entityName) ? errorCode.GetDescription() : errorCode.GetDescription(entityName);
            return new ResponseException
            {
                Message = message + ": " + errorMessage,
                IsSuccess = false
            };
        }
        private static string GetDescription(this Enum value, params object[] args)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            string description = attribute == null ? value.ToString() : attribute.Description;

            return string.Format(description, args);
        }
    }
}
