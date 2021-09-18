using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Web.SystemModule;
using NyTEC.EnergyTrecker.Domain.Entities;

namespace Frontend.Module.Web.ModelUpdates
{
    public class DisableShowSelectionColumn : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator>
    {
        private readonly List<string> targets = new List<string>();

        public DisableShowSelectionColumn()
        {
            targets.Add($"{nameof(AppConfig)}_ListView");
        }

        public override void UpdateNode(ModelNode node)
        {
            foreach (var target in targets)
            {
                var listView = node.Nodes.FirstOrDefault(n => n.Id == target) as IModelListViewWeb;

                if (listView != null)
                    listView.ShowSelectionColumn = true;
            }
        }
    }
}