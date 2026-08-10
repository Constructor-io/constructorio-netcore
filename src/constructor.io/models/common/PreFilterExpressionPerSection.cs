namespace Constructorio_NET.Models
{
    /// <summary>
    /// Associates a <see cref="PreFilterExpression"/> with a specific autocomplete section.
    /// Serialized to the documented bracket-notation shape (e.g. pre_filter_expression[Products]={...}).
    /// </summary>
    public class PreFilterExpressionPerSection
    {
        /// <summary>
        /// Gets or sets the section the expression applies to (e.g. "Products", "Search Suggestions").
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the pre-filter expression to scope results for the section.
        /// </summary>
        public PreFilterExpression Expression { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreFilterExpressionPerSection"/> class.
        /// </summary>
        /// <param name="section">Section the expression applies to.</param>
        /// <param name="expression">Pre-filter expression for the section.</param>
        public PreFilterExpressionPerSection(string section, PreFilterExpression expression)
        {
            this.Section = section;
            this.Expression = expression;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreFilterExpressionPerSection"/> class.
        /// </summary>
        public PreFilterExpressionPerSection()
        {
        }
    }
}
