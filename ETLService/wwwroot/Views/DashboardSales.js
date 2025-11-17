import { WTableDynamicComp} from '../WDevCore/WComponents/WTableDynamic.js';
import { ModelProperty } from '../WDevCore/WModules/CommonModel.js';
window.addEventListener("load", async () => {

    const dataPromise = await fetch("/SalesFact/GetAggregatedSales");
    const data = await dataPromise.json();
    console.log("Datos cargados desde la API:", data);

    class ModelObject {
        /**@type {ModelProperty}*/ year = { type: 'select' };
        /**@type {ModelProperty}*/ category = {
            type: 'wselect',
            ModelObject:
                { category: { type: "text", primary: true }, desc: { type: "text" } },
            Dataset: data.map(d => ({ category: d.category, desc: d.category }))
        };
        /**@type {ModelProperty}*/ totalSale = { type: 'money' };
        /**@type {ModelProperty}*/ unitsSold = { type: 'number' };
    }

    const TableConfigG = {
        Dataset: data, 
        EvalValue: "totalSale",
        AttNameEval: "category",
        groupParams: ["year"], 
        AddChart: true,
        ModelObject: new ModelObject()
    };

    const WTableReport = new WTableDynamicComp(TableConfigG);
    app.append(WTableReport);
});