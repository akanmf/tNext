using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Model.Enums
{
    /// <summary>
    /// Error codes
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>
        /// invalid model
        /// </summary>
        INVALID_MODEL_STATE,
        /// <summary>
        /// unique violation
        /// </summary>
        UNIQUE_VIOLATION,
        /// <summary>
        /// timeout
        /// </summary>
        TIMEOUT,
        /// <summary>
        /// missing parameter
        /// </summary>
        MISSING_PARAMETER,
        /// <summary>
        /// missing transaction id
        /// </summary>
        MISSING_TRANSACTION_ID,
        /// <summary>
        /// missing operator id
        /// </summary>
        MISSING_OPERATOR_ID,
        /// <summary>
        /// missing object
        /// </summary>
        MISSING_OBJECT,
        /// <summary>
        /// item is already deleted
        /// </summary>
        ALREADY_DELETED,
        /// <summary>
        /// generic error
        /// </summary>
        GENERIC_ERROR,
        /// <summary>
        /// missing request header
        /// </summary>
        MISSING_REQUEST_HEADER,
        /// <summary>
        /// missing client name
        /// </summary>
        MISSING_CLIENT_NAME,
        /// <summary>
        /// missing client ip
        /// </summary>
        MISSING_CLIENT_IP,
        /// <summary>
        /// missing request id
        /// </summary>
        MISSING_REQUEST_ID,
        /// <summary>
        /// missing customer id
        /// </summary>
        MISSING_CUSTOMER_ID
    }
}
