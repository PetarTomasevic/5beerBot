using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ViberBot.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusCodeEnum
    {
        /// <summary>
        /// Success
        /// </summary>
        [EnumMember(Value = "ok")]
        Ok = 0,

        /// <summary>
        /// The webhook URL is not valid
        /// </summary>
        [EnumMember(Value = "invalidUrl")]
        InvalidUrl = 1,

        /// <summary>
        /// The authentication token is not valid
        /// </summary>
        [EnumMember(Value = "invalidAuthToken")]
        InvalidAuthToken = 2,

        /// <summary>
        /// There is an error in the request itself (missing comma, brackets, etc.)
        /// </summary>
        [EnumMember(Value = "badData")]
        BadData = 3,

        /// <summary>
        /// Some mandatory data is missing
        /// </summary>
        [EnumMember(Value = "missingData")]
        MissingData = 4,

        /// <summary>
        /// The receiver is not registered to Viber
        /// </summary>
        [EnumMember(Value = "receiverNotRegistered")]
        ReceiverNotRegistered = 5,

        /// <summary>
        /// The receiver is not subscribed to the account
        /// </summary>
        [EnumMember(Value = "receiverNotSubscribed")]
        ReceiverNotSubscribed = 6,

        /// <summary>
        /// The account is blocked
        /// </summary>
        [EnumMember(Value = "publicAccountBlocked")]
        PublicAccountBlocked = 7,

        /// <summary>
        /// The account associated with the token is not a account
        /// </summary>
        [EnumMember(Value = "publicAccountNotFound")]
        PublicAccountNotFound = 8,

        /// <summary>
        /// The account is suspended
        /// </summary>
        [EnumMember(Value = "publicAccountSuspended")]
        PublicAccountSuspended = 9,

        /// <summary>
        /// No webhook was set for the account
        /// </summary>
        [EnumMember(Value = "webhookNotSet")]
        WebhookNotSet = 10,

        /// <summary>
        /// The receiver is using a device or a Viber version that don’t support accounts
        /// </summary>
        [EnumMember(Value = "receiverNoSuitableDevice")]
        ReceiverNoSuitableDevice = 11,

        /// <summary>
        /// Rate control breach
        /// </summary>
        [EnumMember(Value = "tooManyRequests")]
        TooManyRequests = 12,

        /// <summary>
        /// Maximum supported account version by all user’s devices is less than the "minApiVersion" in the message
        /// </summary>
        [EnumMember(Value = "apiVersionNotSupported")]
        ApiVersionNotSupported = 13,

        /// <summary>
        /// "minApiVersion" is not compatible to the message fields
        /// </summary>
        [EnumMember(Value = "incompatibleWithVersion")]
        IncompatibleWithVersion = 14,

        /// <summary>
        /// The account is not authorized
        /// </summary>
        [EnumMember(Value = "publicAccountNotAuthorized")]
        PublicAccountNotAuthorized = 15,

        /// <summary>
        /// Inline message not allowed
        /// </summary>
        [EnumMember(Value = "inchatReplyMessageNotAllowed")]
        InchatReplyMessageNotAllowed = 16,

        /// <summary>
        /// The account is not inline
        /// </summary>
        [EnumMember(Value = "publicAccountIsNotInline")]
        PublicAccountIsNotInline = 17,

        /// <summary>
        /// Failed to post to public account. The bot is missing a Public Chat interface
        /// </summary>
        [EnumMember(Value = "noPublicChat")]
        NoPublicChat = 18,

        /// <summary>
        /// Cannot send broadcast message
        /// </summary>
        [EnumMember(Value = "cannotSendBroadcast")]
        CannotSendBroadcast = 19,

        /// <summary>
        /// Attempt to send broadcast message from the bot
        /// </summary>
        [EnumMember(Value = "broadcastNotAllowed")]
        BroadcastNotAllowed = 20,

        /// <summary>
        /// General error
        /// </summary>
        [EnumMember(Value = "other")]
        GeneralError = -1
    }
}