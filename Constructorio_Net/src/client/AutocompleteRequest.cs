using System;
using System.Collections.Generic;

namespace Constructorio_Net
{

    /**
     * Constructor.io Autocomplete Request
     */
    public class AutocompleteRequest
    {

        private string query;
        private Dictionary<string, int> resultsPerSection;

        /**
         * Creates an autocomplete request
         *
         * @param query the term to return suggestions for
         */
        public AutocompleteRequest(string query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required");
            }

            this.query = query;
            this.resultsPerSection = new Dictionary<string, int>();
        }

        /**
         * @param query the query to set
         */
        public void setQuery(string query)
        {
            this.query = query;
        }

        /**
         * @return the query
         */
        public string getQuery()
        {
            return query;
        }

        /**
         * @param resultsPerSection the resultsPerSection to set
         */
        public void setResultsPerSection(Dictionary<string, int> resultsPerSection)
        {
            this.resultsPerSection = resultsPerSection;
        }

        /**
         * @return the resultsPerSection
         */
        public Dictionary<string, int> getResultsPerSection()
        {
            return resultsPerSection;
        }
    }
}