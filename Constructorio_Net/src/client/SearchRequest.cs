using System;
using System.Collections.Generic;

namespace Constructorio_NET
{
    /**
     * Constructor.io Search Request
     */
    public class SearchRequest
    {

        private string query;
        private string section;
        private int page;
        private int resultsPerPage;
        private Dictionary<string, List<string>> facets;
        private string groupId;
        private string sortBy;
        private Boolean sortAscending;
        private string collectionId;

        /**
         * Creates a search request
         *
         * @param query the term to return search results for
         */
        public SearchRequest(string query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required");
            }
            this.section = "Products";
            this.page = 1;
            this.resultsPerPage = 30;
            this.facets = new Dictionary<string, List<string>>();
            this.sortAscending = true;
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

        /**
         * @return the groupId
         */
        public string getGroupId()
        {
            return groupId;
        }

        /**
         * @param groupId the groupId to set
         */
        public void setGroupId(string groupId)
        {
            this.groupId = groupId;
        }

        /**
         * @param facets the facets to set
         */
        public void setFacets(Dictionary<string, List<string>> facets)
        {
            this.facets = facets;
        }

        /**
         * @return the facets
         */
        public Dictionary<string, List<string>> getFacets()
        {
            return facets;
        }

        /**
         * @param sortBy the sortBy to set
         */
        public void setSortBy(string sortBy)
        {
            this.sortBy = sortBy;
        }

        /**
         * @return the sortBy
         */
        public string getSortBy()
        {
            return sortBy;
        }

        /**
         * @param sortAscending the sortAscending to set
         */
        public void setSortAscending(Boolean sortAscending)
        {
            this.sortAscending = sortAscending;
        }

        /**
         * @return the sortAscending
         */
        public Boolean getSortAscending()
        {
            return sortAscending;
        }

        /**
         * @param collectionId the collectionId to set
         */
        public void setCollectionId(string collectionId)
        {
            this.collectionId = collectionId;
        }

        /**
         * @return the collectionId
         */
        public string getCollectionId()
        {
            return collectionId;
        }
    }
}