using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io QuizRequest Request Class.
    /// </summary>
    public class QuizRequest
    {
        /// <summary>
        /// Gets or sets the quiz id used to get quiz result.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets An array of answers in the format [[1,2],[1]].
        /// </summary>
        public List<List<string>> Answers { get; set; }

        /// <summary>
        /// Gets or sets version identifier for the quiz.
        /// </summary>
        public string QuizVersionId { get; set; }

        /// <summary>
        /// Session id to mark the current session of a quiz. If not specified, generates new quiz_session_id in response.
        /// </summary>
        public string QuizSessionId { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizRequest"/> class.
        /// </summary>
        /// <param name="id">Quiz identifier to use for the request.</param>
        public QuizRequest(string id)
        {
            if (id == null)
            {
                throw new ArgumentException("quiz id is required");
            }

            this.Id = id;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();
            if (this.UserInfo != null)
            {
                if (this.UserInfo.GetUserId() != null)
                {
                    parameters.Add(Constants.USER_ID, this.UserInfo.GetUserId());
                }

                if (this.UserInfo.GetClientId() != null)
                {
                    parameters.Add(Constants.CLIENT_ID, this.UserInfo.GetClientId());
                }

                if (this.UserInfo.GetSessionId() != 0)
                {
                    parameters.Add(Constants.SESSION_ID, this.UserInfo.GetSessionId());
                }

                if (this.UserInfo.GetUserSegments() != null)
                {
                    parameters.Add(Constants.USER_SEGMENTS, this.UserInfo.GetUserSegments());
                }
            }

            if (this.Answers != null)
            {
                parameters.Add(Constants.ANSWERS, this.Answers);
            }

            if (this.QuizVersionId != null)
            {
                parameters.Add(Constants.QUIZ_VERSION_ID, this.QuizVersionId);
            }

            if (this.QuizSessionId != null)
            {
                parameters.Add(Constants.QUIZ_SESSION_ID, this.QuizSessionId);
            }

            return parameters;
        }

        /// <summary>
        /// Get request headers.
        /// </summary>
        /// <returns>Dictionary of request headers.</returns>
        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            if (this.UserInfo != null)
            {
                if (this.UserInfo.GetForwardedFor() != null)
                {
                    requestHeaders.Add(Constants.USER_IP, this.UserInfo.GetForwardedFor());
                }

                if (this.UserInfo.GetUserAgent() != null)
                {
                    requestHeaders.Add(Constants.USER_AGENT, this.UserInfo.GetUserAgent());
                }
            }

            return requestHeaders;
        }
    }
}
