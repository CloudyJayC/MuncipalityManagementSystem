using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;

namespace MunicipalityManagementSystem.Services
{
    /// <summary>
    /// Handles creation and management of citizen notifications.
    /// Injected into controllers that need to trigger notifications.
    /// </summary>
    public class NotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ApplicationDbContext context, ILogger<NotificationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Creates and persists a notification for a specific user.
        /// </summary>
        /// <param name="userId">The Identity user ID of the citizen to notify.</param>
        /// <param name="message">The notification message to display.</param>
        /// <param name="serviceRequestId">Optional — the related service request ID for linking.</param>
        /// <returns>The saved Notification, or null if creation failed.</returns>
        public async Task<Notification?> CreateNotificationAsync(
            string userId,
            string message,
            int? serviceRequestId = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogWarning("CreateNotificationAsync called with null or empty userId — skipping.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                _logger.LogWarning("CreateNotificationAsync called with null or empty message — skipping.");
                return null;
            }

            try
            {
                var notification = new Notification
                {
                    UserId = userId,
                    Message = message,
                    IsRead = false,
                    CreatedAt = DateTime.Now,
                    ServiceRequestId = serviceRequestId
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Notification created for user {UserId} — RequestId: {ServiceRequestId}",
                    userId, serviceRequestId);

                return notification;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to create notification for user {UserId} — RequestId: {ServiceRequestId}",
                    userId, serviceRequestId);
                return null;
            }
        }

        /// <summary>
        /// Returns the count of unread notifications for a given user.
        /// Used by the navbar badge.
        /// </summary>
        /// <param name="userId">The Identity user ID to check.</param>
        /// <returns>Count of unread notifications.</returns>
        public async Task<int> GetUnreadCountAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return 0;

            try
            {
                return await _context.Notifications
                    .CountAsync(n => n.UserId == userId && !n.IsRead);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get unread notification count for user {UserId}", userId);
                return 0;
            }
        }
    }
}
