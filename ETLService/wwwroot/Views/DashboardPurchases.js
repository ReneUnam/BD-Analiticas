import { WTableDynamicComp } from '../WDevCore/WComponents/WTableDynamic.js';
import { ModelProperty } from '../WDevCore/WModules/CommonModel.js';

window.onload = async () => {
    // 1. Obtener data desde el nuevo endpoint
    const dataPromise = await fetch("/PurchasesFact/GetAggregatedPurchases");
    const data = await dataPromise.json();

    console.log("Datos de compras cargados desde la API:", data);

    class PurchaseModelObject {
    /**@type {ModelProperty}*/ year = { type: 'select' };
    /**@type {ModelProperty}*/ supplierName = {
            type: 'wselect',
            ModelObject: { supplierName: { type: "text", primary: true }, desc: { type: "text" } }
            // Dataset se mapea desde 'data' al igual que en ventas
        };
    /**@type {ModelProperty}*/ laboratory = { type: 'select' }; // Para ver qué laboratorio se compra más
    /**@type {ModelProperty}*/ totalCostPurchase = { type: 'money' }; // Métrica principal
    /**@type {ModelProperty}*/ unitsPurchased = { type: 'number', hiddenInTable: true };
    }

    const modelInstance = new PurchaseModelObject();

    // 2. Mapear el Dataset del filtro
    modelInstance.supplierName.Dataset = data.map(d => ({
        supplierName: d.supplierName,
        desc: d.supplierName
    }));

    // 3. Configuración del componente
    const TableConfigPurchase = {
        Dataset: data,
        EvalValue: "totalCostPurchase", // ¡Métrica principal: Costo de Compra!
        AttNameEval: "supplierName",  // Filas por Proveedor
        groupParams: ["year", "laboratory"], // Agrupar por Año o Laboratorio
        AddChart: true,
        ModelObject: modelInstance
    };

    const WTableReport = new WTableDynamicComp(TableConfigPurchase);
    app.append(WTableReport);
};