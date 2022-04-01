using System;
using System.Collections.Generic;

namespace Constructorio_NET
{
    /**
     * Constructor.io Recommendations Request
     */
    public class RecommendationsRequest
    {

        private string podId;
        private int numResults;
        private List<string> itemIds;
        private string section;

        /**
         * Creates a recommendations request
         *
         * @param podId the pod id to retrieve results from
         */
        public RecommendationsRequest(string podId)
        {
            if (podId == null)
            {
                throw new ArgumentException("podId is a required parameter of type string");
            }

            this.podId = podId;
            this.numResults = 10;
            this.itemIds = null;
            this.section = "Products";
        }

        /**
         * @param podId the pod id to set
         */
        public void setPodId(string podId)
        {
            this.podId = podId;
        }

        /**
         * @return the pod id
         */
        public string getPodId()
        {
            return podId;
        }

        /**
         * @param numResults the num results to set
         */
        public void setNumResults(int numResults)
        {
            this.numResults = numResults;
        }

        /**
         * @return the num results
         */
        public int getNumResults()
        {
            return numResults;
        }

        /**
         * @param itemIds the item id's to set
         */
        public void setItemIds(List<string> itemIds)
        {
            this.itemIds = itemIds;
        }

        /**
         * @return the item id's
         */
        public List<string> getItemIds()
        {
            return itemIds;
        }

        /**
         * @param section the section to set
         */
        public void setSection(string section)
        {
            this.section = section;
        }

        /**
         * @return the section
         */
        public string getSection()
        {
            return section;
        }
    }
}