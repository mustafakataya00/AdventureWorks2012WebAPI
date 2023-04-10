﻿namespace AdventureWorkAPI.Dto
{
    public class InventoryDto
    {
        public int ProductId { get; set; }

        /// <summary>
        /// Inventory location identification number. Foreign key to Location.LocationID. 
        /// </summary>
        public short LocationId { get; set; }

        /// <summary>
        /// Storage compartment within an inventory location.
        /// </summary>
        public string Shelf { get; set; } = null!;

        /// <summary>
        /// Storage container on a shelf in an inventory location.
        /// </summary>
        public byte Bin { get; set; }

        /// <summary>
        /// Quantity of products in the inventory location.
        /// </summary>
        public short Quantity { get; set; }

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
