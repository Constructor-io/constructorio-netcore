using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Modules
{
    public class Quizzes : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quizzes"/> class.
        /// Interface for quizzes related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        internal Quizzes(Hashtable options)
        {
            Hashtable quizOptions = new Hashtable(options);
            quizOptions[Constants.SERVICE_URL] = "https://quizzes.cnstrc.com/v1/quizzes";
            this.Options = quizOptions;
        }

        internal string CreateQuizUrl(QuizRequest req, string endpoint)
        {
            Hashtable requestParams = req.GetRequestParameters();
            List<string> paths = new List<string> { req.QuizId, endpoint };

            return MakeUrl(this.Options, paths, requestParams);
        }

        /// <summary>
        /// Retrieve Quiz next question from API.
        /// </summary>
        /// <param name="quizzesRequest">Constructorio's quizzes request object.</param>
        /// <returns>Constructorio's quiz next question object.</returns>
        public async Task<NextQuestionResponse> GetNextQuestion(QuizRequest quizzesRequest)
        {
            string url;
            string result;

            url = CreateQuizUrl(quizzesRequest, "next");
            Dictionary<string, string> requestHeaders = quizzesRequest.GetRequestHeaders();
            result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<NextQuestionResponse>(result);
            }

            throw new ConstructorException("GetNextQuestion response data is malformed");
        }

        /// <summary>
        /// Retrieve Quiz results from API.
        /// </summary>
        /// <param name="quizzesRequest">Constructorio's quizzes request object.</param>
        /// <returns>Constructorio's quiz results response object.</returns>
        public async Task<QuizResultsResponse> GetResults(QuizRequest quizzesRequest)
        {
            string url;
            string result;

            url = CreateQuizUrl(quizzesRequest, "finalize");
            Dictionary<string, string> requestHeaders = quizzesRequest.GetRequestHeaders();
            result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<QuizResultsResponse>(result);
            }

            throw new ConstructorException("GetResults response data is malformed");
        }
    }
}
