using System;

namespace Constructorio_NET
{
    /**
     * Constructor.io Natural Language Search Request
     */
    public class NaturalLanguageSearchRequest
    {

        private string query;
        private string section;
        private int page;
        private int resultsPerPage;

        /**
         * Creates a natural language search request
         *
         * @param query the term to return natural language search results for
         */
        public NaturalLanguageSearchRequest(string query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required");
            }

            this.query = query;
            this.section = "Products";
            this.page = 1;
            this.resultsPerPage = 30;
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
         * @return the section
         */
        public string getSection()
        {
            return section;
        }

        /**
         * @param section the section to set
         */
        public void setSection(string section)
        {
            this.section = section;
        }

        /**
         * @return the page
         */
        public int getPage()
        {
            return page;
        }

        /**
         * @param page the page to set
         */
        public void setPage(int page)
        {
            this.page = page;
        }

        /**
         * @return the resultsPerPage
         */
        public int getResultsPerPage()
        {
            return resultsPerPage;
        }

        /**
         * @param resultsPerPage the resultsPerPage to set
         */
        public void setResultsPerPage(int resultsPerPage)
        {
            this.resultsPerPage = resultsPerPage;
        }
    }
}