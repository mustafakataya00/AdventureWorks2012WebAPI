namespace AdventureWorkAPI.Dto
{
    public class SubCategoryDto
    {
        public int ProductSubcategoryId { get; set; }

        /// <summary>
        /// Product category identification number. Foreign key to ProductCategory.ProductCategoryID.
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// Subcategory description.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        public Guid Rowguid { get; set; }

        /// <summary>
        /// Date and time the record was last updated.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
