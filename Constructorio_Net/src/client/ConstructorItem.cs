using System;
using System.Collections.Generic;

namespace Constructorio_Net
{
    /**
     * Constructor.io Item
     */
    public class ConstructorItem
    {

        private string itemName;
        private int? suggestedScore;
        private List<string> keywords;
        private string url;
        private string imageUrl;
        private string id;
        private string description;
        private Dictionary<string, string> facets;
        private Dictionary<string, string> metadata;
        private List<string> groupIds;

        /**
         * Creates an autocomplete item.  Optional public fields are in the <a href="https://docs.constructor.io/rest-api.html#add-an-item">API documentation</a>
         *
         * @param itemName the name of the item that you are adding.
         */
        public ConstructorItem(string itemName)
        {
            if (itemName == null)
            {
                throw new ArgumentException("itemName is required");
            }

            this.itemName = itemName;
            this.suggestedScore = null;
            this.keywords = null;
            this.url = null;
            this.imageUrl = null;
            this.description = null;
            this.id = null;
            this.facets = null;
            this.metadata = null;
            this.groupIds = null;
        }

        /**
         * Returns the HashMap form of an autocomplete item for converting to JSON
         */
        public Dictionary<string, Object> toMap()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (itemName == null)
            {
                throw new ArgumentException("itemName is required");
            }

            parameters.Add("item_name", this.itemName);
            parameters.Add("suggested_score", this.suggestedScore);
            parameters.Add("keywords", this.keywords);
            parameters.Add("url", this.url);
            parameters.Add("image_url", this.imageUrl);
            parameters.Add("description", this.description);
            parameters.Add("id", this.id);
            parameters.Add("facets", this.facets);
            parameters.Add("metadata", this.metadata);
            parameters.Add("group_ids", this.groupIds);

            return parameters;
        }

        /**
         * @return the itemName
         */
        public string getItemName()
        {
            return itemName;
        }

        /**
         * @param itemName the itemName to set
         */
        public void setItemName(string itemName)
        {
            this.itemName = itemName;
        }

        /**
         * @return the suggestedScore
         */
        public int? getSuggestedScore()
        {
            return suggestedScore;
        }

        /**
         * @param suggestedScore the suggestedScore to set
         */
        public void setSuggestedScore(int suggestedScore)
        {
            this.suggestedScore = suggestedScore;
        }

        /**
         * @return the keywords
         */
        public List<string> getKeywords()
        {
            return keywords;
        }

        /**
         * @param keywords the keywords to set
         */
        public void setKeywords(List<string> keywords)
        {
            this.keywords = keywords;
        }

        /**
         * @return the url
         */
        public string getUrl()
        {
            return url;
        }

        /**
         * @param url the url to set
         */
        public void setUrl(string url)
        {
            this.url = url;
        }

        /**
         * @return the imageUrl
         */
        public string getImageUrl()
        {
            return imageUrl;
        }

        /**
         * @param imageUrl the imageUrl to set
         */
        public void setImageUrl(string imageUrl)
        {
            this.imageUrl = imageUrl;
        }

        /**
         * @return the description
         */
        public string getDescription()
        {
            return description;
        }

        /**
         * @param description the description to set
         */
        public void setDescription(string description)
        {
            this.description = description;
        }

        /**
         * @return the id
         */
        public string getId()
        {
            return id;
        }

        /**
         * @param id the id to set
         */
        public void setId(string id)
        {
            this.id = id;
        }

        /**
         * @return the facets
         */
        public Dictionary<string, string> getFacets()
        {
            return facets;
        }

        /**
         * @param facets the facets to set
         */
        public void setFacets(Dictionary<string, string> facets)
        {
            this.facets = facets;
        }

        /**
         * @return the metadata
         */
        public Dictionary<string, string> getMetadata()
        {
            return metadata;
        }

        /**
         * @param metadata the metadata to set
         */
        public void setMetadata(Dictionary<string, string> metadata)
        {
            this.metadata = metadata;
        }

        /**
         * @return the groupIds
         */
        public List<string> getGroupIds()
        {
            return groupIds;
        }

        /**
         * @param groupIds the groupIds to set
         */
        public void setGroupIds(List<string> groupIds)
        {
            this.groupIds = groupIds;
        }
    }
}