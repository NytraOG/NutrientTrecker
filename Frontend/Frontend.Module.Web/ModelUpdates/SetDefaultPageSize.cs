using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Web.SystemModule;

namespace Frontend.Module.Web.ModelUpdates
{
    public class SetDefaultPageSize : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            foreach (IModelListViewWeb modelListView in ((IModelViews)node).Where(mv => mv is IModelListViewWeb))
                modelListView.PageSize = 100;
        }
    }
}