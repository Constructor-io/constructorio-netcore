using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Request parameters and headers shared by recommendations pod and page requests.
    /// </summary>
    internal static class RecommendationsRequestParameters
    {
        internal static void AddSharedParameters(
            Hashtable parameters,
            UserInfo userInfo,
            string section,
            Dictionary<string, string> testCells,
            List<string> itemIds,
            List<string> variationIds,
            string term)
        {
            if (userInfo != null)
            {
                if (userInfo.GetUserId() != null)
                {
                    parameters.Add(Constants.USER_ID, userInfo.GetUserId());
                }

                if (userInfo.GetClientId() != null)
                {
                    parameters.Add(Constants.CLIENT_ID, userInfo.GetClientId());
                }

                if (userInfo.GetSessionId() != 0)
                {
                    parameters.Add(Constants.SESSION_ID, userInfo.GetSessionId());
                }

                if (userInfo.GetUserSegments() != null)
                {
                    parameters.Add(Constants.USER_SEGMENTS, userInfo.GetUserSegments());
                }
            }

            if (section != null)
            {
                parameters.Add(Constants.SECTION, section);
            }

            if (testCells != null)
            {
                parameters.Add(Constants.TEST_CELLS, testCells);
            }

            if (itemIds != null && itemIds.Count != 0)
            {
                parameters.Add("item_id", itemIds);
            }

            if (variationIds != null && variationIds.Count != 0)
            {
                parameters.Add("variation_id", variationIds);
            }

            if (term != null)
            {
                parameters.Add("term", term);
            }
        }

        internal static Dictionary<string, string> GetRequestHeaders(UserInfo userInfo)
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            if (userInfo != null)
            {
                if (userInfo.GetForwardedFor() != null)
                {
                    requestHeaders.Add(Constants.USER_IP, userInfo.GetForwardedFor());
                }

                if (userInfo.GetUserAgent() != null)
                {
                    requestHeaders.Add(Constants.USER_AGENT, userInfo.GetUserAgent());
                }
            }

            return requestHeaders;
        }
    }
}
