using System;

namespace SogetiDemo.TDD.BankApp.Common.Utils
{
    /// <summary>
    /// Assertion utility class that assists in validating arguments. Useful for identifying programmer errors early and clearly at runtime.
    /// 
    /// See: http://static.springsource.org/spring-framework/docs/3.2.0.M2/api/org/springframework/util/Assert.html
    /// </summary>
    public class Assert
    {
        public static void NotNull(object objectToCheck, string paramName = "parameter")
        {
            if (objectToCheck == null)
            {
                throw new ArgumentNullException(paramName, "Cannot be null!");
            }
        }
    }
}
