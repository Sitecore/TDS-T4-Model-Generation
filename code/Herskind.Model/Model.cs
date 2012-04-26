using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Herskind.Model.Helper;
using Herskind.Model.Helper.FieldTypes;
 

#region Sample Item (Sample)
namespace Herskind.Model.Sample
{
	public partial interface ISampleItem : IItemWrapper 
	{		
		ITextFieldWrapper Text { get; }		
		ITextFieldWrapper Title { get; }		
			
	}

	[TemplateMapping("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}", typeof(ISampleItem))]
	public class SampleItem : BaseItemWrapper, ISampleItem
	{
		private Item _innerItem = null;
		public SampleItem()
        {
        }
//		public SampleItem(Item item) : base(item)
//		{
//			_innerItem = item;
//		}

		public ITextFieldWrapper Text
		{
			get 
			{
				return (ITextFieldWrapper)GetField("a60acd61-a6db-4182-8329-c957982cec74"); 
			} 
		} 	

		public ITextFieldWrapper Title
		{
			get 
			{
				return (ITextFieldWrapper)GetField("75577384-3c97-45da-a847-81b00500e250"); 
			} 
		} 	
	}
}
#endregion

namespace Herskind.Model
{
	public static class FieldWrapperInterfaceMap
	{
		public static Dictionary<string, Type> Mappings = new Dictionary<string, Type>()
		{
			{"checkbox", typeof(IBooleanFieldWrapper)},
			{"image", typeof(IImageFieldWrapper)},
			{"date", typeof(IDateFieldWrapper)},
			{"datetime", typeof(IDateFieldWrapper)},
			{"checklist", typeof(IListFieldWrapper)},
			{"treelist", typeof(IListFieldWrapper)},
			{"treelistex", typeof(IListFieldWrapper)},
			{"multilist", typeof(IListFieldWrapper)},
			{"droplink", typeof(ILinkFieldWrapper)},
			{"droptree", typeof(ILinkFieldWrapper)},
			{"general link", typeof(ILinkFieldWrapper)},
			{"single-line text", typeof(ITextFieldWrapper)},
			{"multi-line text", typeof(ITextFieldWrapper)},
			{"rich text", typeof(ITextFieldWrapper)},
			{"", typeof(ITextFieldWrapper)},
		};
	}
}

