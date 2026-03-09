using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MunicipalityManagementSystem.Hubs
{
    /// <summary>
    /// SignalR hub for real-time citizen notifications.
    /// Each authenticated user is placed into a private group named by their UserId
    /// so the server can push messages to a specific citizen without broadcasting.
    /// </summary>
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called automatically when a client connects to the hub.
        /// Adds the user to a group named by their UserId for targeted push messages.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                _logger.LogInformation(
                    "SignalR: user {UserId} connected — ConnectionId: {ConnectionId}",
                    userId, Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called automatically when a client disconnects from the hub.
        /// Removes the user from their group and logs the disconnection.
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

                if (exception != null)
                {
                    _logger.LogWarning(exception,
                        "SignalR: user {UserId} disconnected with error — ConnectionId: {ConnectionId}",
                        userId, Context.ConnectionId);
                }
                else
                {
                    _logger.LogInformation(
                        "SignalR: user {UserId} disconnected cleanly — ConnectionId: {ConnectionId}",
                        userId, Context.ConnectionId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
