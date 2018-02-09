namespace Sitecore.Support.Data.Fields
{
    using Sitecore.Data.Fields;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    public class ReferenceField : CustomField
    {
        #region Original code
        public ReferenceField(Field innerField) : base(innerField)
        {
            Assert.ArgumentNotNull(innerField, "innerField");
        }

        public Sitecore.Data.Database Database =>
         (this.GetDatabase() ?? base.InnerField.Database);

        public string Path
        {
            get { return base.Value; }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                base.Value = value;
            }
        }

        public ID TargetID
        {
            get
            {
                Item item = this.Database.Items[this.Path];
                if (item == null)
                {
                    return ID.Null;
                }
                return item.ID;
            }
        }

        public Item TargetItem =>
            this.Database.Items[this.Path];

        public void Clear()
        {
            base.Value = string.Empty;
        }

        public static implicit operator ReferenceField(Field field)
        {
            if (field == null)
            {
                return null;
            }
            return new ReferenceField(field);
        }

        public override void RemoveLink(ItemLink itemLink)
        {
            Assert.ArgumentNotNull(itemLink, "itemLink");
            if (itemLink.TargetItemID == this.TargetID)
            {
                this.Clear();
            }
        }

        public override void ValidateLinks(LinksValidationResult result)
        {
            Assert.ArgumentNotNull(result, "result");
            if (!string.IsNullOrEmpty(this.Path) && (this.Path != "{00000000-0000-0000-0000-000000000000}"))
            {
                Item targetItem = this.TargetItem;
                if (targetItem != null)
                {
                    result.AddValidLink(targetItem, this.Path);
                }
                else
                {
                    result.AddBrokenLink(this.Path);
                }
            }
        }
        #endregion
        #region Modified code
        public override void Relink(ItemLink itemLink, Item newLink)
        {
            Assert.ArgumentNotNull(itemLink, "itemLink");
            Assert.ArgumentNotNull(newLink, "newLink");
            if (itemLink.TargetItemID == this.TargetID)
            {
                this.Path = newLink.ID.ToString();
            }
        }
        #endregion
    }
}


