namespace Sitecore.Support.Data.Fields
{
    using Sitecore.Data.Fields;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    public class ReferenceField : Sitecore.Data.Fields.ReferenceField
    {
        public ReferenceField(Field innerField) : base(innerField)
        {
            Assert.ArgumentNotNull(innerField, "innerField");
        }

        public override void Relink(ItemLink itemLink, Item newLink)
        {
            Assert.ArgumentNotNull(itemLink, "itemLink");
            Assert.ArgumentNotNull(newLink, "newLink");
            if (itemLink.TargetItemID == this.TargetID)
            {
                this.Path = newLink.ID.ToString();
            }
        }
    }
}


