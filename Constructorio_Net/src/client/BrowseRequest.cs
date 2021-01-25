using System;
using System.Collections.Generic;

/**
 * Constructor.io Browse Request
 */
public class BrowseRequest
{

    private string filterName;
    private string filterValue;
    private string section;
    private int page;
    private int resultsPerPage;
    private Dictionary<string, List<string>> facets;
    private string groupId;
    private string sortBy;
    private Boolean sortAscending;

    /**
     * Creates a browse request
     *
     * @param query the term to return browse results for
     */
    public BrowseRequest(string filterName, string filterValue)
    {
        if (filterName == null) {
            throw new ArgumentException("filterName is required");
        }

        if (filterValue == null)
        {
            throw new ArgumentException("filterValue is required");
        }

    this.filterName = filterName;
    this.filterValue = filterValue;
    this.section = "Products";
    this.page = 1;
    this.resultsPerPage = 30;
    this.facets = new Dictionary<string, List<string>>();
    this.sortAscending = true;
        }

        /**
         * @param filterName the filterName to set
         */
        public void setFilterName(string filterName)
    {
        this.filterName = filterName;
    }

    /**
     * @return the filterName
     */
    public string getFilterName()
    {
        return filterName;
    }

    /**
     * @param filterValue the filterValue to set
     */
    public void setFilterValue(string filterValue)
    {
        this.filterValue = filterValue;
    }

    /**
     * @return the filterValue
     */
    public string getFilterValue()
    {
        return filterValue;
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

}