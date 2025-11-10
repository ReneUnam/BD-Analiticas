import { WTableDynamicComp, WttTableDynamicComp } from '../WDevCore/WComponents/WTableDynamic.js';
import { ModelProperty } from '../WDevCore/WModules/CommonModel.js';

window.onload = async () => {
    const dataPromise = await fetch("../Resource/data.json");
    const data = await dataPromise.json();
    console.log(data);

    class ModelObject {
        /**@type {ModelProperty} */ product = { type: 'draw' };
        /**@type {ModelProperty} */ category = {
            type: 'wselect',
            ModelObject:
                { category: { type: "text", primary: true }, desc: { type: "text" } },
            Dataset: data.map(d => ({ category: d.category, desc: d.category }))
        };
        /**@type {ModelProperty}*/ year = { type: 'select' };
        /**@type {ModelProperty}*/ mes = { type: 'select' };
        /**@type {ModelProperty}*/ quarter = { type: 'text' };
        /**@type {ModelProperty}*/ units_sold = {
            type: 'number',
            hiddenInTable: true
        };
        /**@type {ModelProperty}*/ unit_price = {
            type: 'money',
            hiddenInTable: true
        };
        /**@type {ModelProperty}*/ total_sale = {
            type: 'money',
            hiddenInTable: true
        };
    }

    const TableConfigG ={
        DataSet: data,
        EvalValue: "total_sale",
        AttNameEval: "category",
        groupParams: ["year"],
        AddChart: true,
        ModelObject: new ModelObject()
    };
    const WTableReport = new WTableDynamicComp(TableConfigG);

    app.append(WTableReport);
}