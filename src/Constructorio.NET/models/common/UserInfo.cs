using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    public class UserInfo
    {
        private int sessionId;

        private string clientId;

        private string userId;

        private List<string> userSegments;

        private string forwardedFor;

        private string userAgent;

        public UserInfo(string clientId, int sessionId)
        {
            this.SetSessionId(sessionId);
            this.SetClientId(clientId);
        }

        public UserInfo()
        {
        }

        public string GetClientId()
        {
            return this.clientId;
        }

        public int GetSessionId()
        {
            return this.sessionId;
        }

        public string GetUserId()
        {
            return this.userId;
        }

        public List<string> GetUserSegments()
        {
            return this.userSegments;
        }

        public string GetForwardedFor()
        {
            return this.forwardedFor;
        }

        public string GetUserAgent()
        {
            return this.userAgent;
        }

        public void SetSessionId(int sessionId)
        {
            if (sessionId <= 0)
            {
                throw new System.ArgumentException("Session ID cannot be less than or equal to 0.");
            }
            else
            {
                this.sessionId = sessionId;
            }
        }

        public void SetClientId(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new System.ArgumentException("Client ID cannot be null or an empty string.");
            }
            else
            {
                this.clientId = clientId;
            }
        }

        public void SetUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new System.ArgumentException("User ID cannot be an empty string.");
            }
            else
            {
                this.userId = userId;
            }
        }

        public void SetUserSegments(List<string> userSegments)
        {
            if (userSegments == null)
            {
                throw new System.ArgumentException("User segments cannot be null.");
            }
            else
            {
                this.userSegments = userSegments;
            }
        }

        public void SetForwardedFor(string forwardedFor)
        {
            if (string.IsNullOrEmpty(forwardedFor))
            {
                throw new System.ArgumentException("Forwarded for cannot be null or an empty string.");
            }
            else
            {
                this.forwardedFor = forwardedFor;
            }
        }

        public void SetUserAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                throw new System.ArgumentException("User agent cannot be null.");
            }
            else
            {
                this.userAgent = userAgent;
            }
        }
    }
}