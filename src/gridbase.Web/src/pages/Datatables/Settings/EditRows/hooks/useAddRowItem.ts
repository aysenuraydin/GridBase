import { Datatable } from "common/data/Datatable";
import { TableCell } from "common/data/TableCell";
import { TableColumn } from "common/data/TableColumn";
import { FileData, TableRowWithStatus } from "components/Common/interfaces/TableRowContextType";
import { useFormik } from "formik";
import { createDynamicYupSchema } from "helpers/validationHelper";
import { useGetForeignTableRowByTableId } from "hooks/useTableRows";
import { Dispatch, MutableRefObject, SetStateAction, useEffect, useMemo, useState } from "react";
import * as Yup from "yup";

export interface IFormCells {
    [rowId: number]: {
        [columnId: number]: any;
    };
}
export interface ITableInitialValues {
    id: number | undefined;
    cells: IFormCells;
}

export const useAddRowItem = (
    columns: TableColumn[], 
    table:Datatable, 
    fileDataRef:MutableRefObject<FileData>, 
    fileManagerRefs:MutableRefObject<{ [key: string]: any }>, 
    setRows:Dispatch<SetStateAction<{ [tableId: number]: TableRowWithStatus[] }>>
) => {
    const { 
        data: tableAndRows, 
        isLoading: isTableAndRowsLoading, 
        error:tableAndRowsError 
    } = useGetForeignTableRowByTableId(Number(table?.id)); 

    const [foreignRows, setForeignRows] = useState< {[rowId: number]: TableCell[]} >({});

    const rowSchema = useMemo(() => createDynamicYupSchema(columns), [columns]);

    const initialValues: ITableInitialValues = {
        id: table?.id,
        cells: {
            [0]: {}
        }
    }; 
    columns.forEach(col => {
        initialValues.cells[0][col.id] = "";
    });  
    const formik = useFormik({
        enableReinitialize: true,
        initialValues,
        validationSchema: Yup.object({
            cells: Yup.object({
                [0]: rowSchema 
            })
        }),  
        onSubmit: async (values) => {
            const tempId = Date.now();

            const currentFiles = fileDataRef.current.selectedFiles;
            const currentRefs = fileManagerRefs.current;

            Object.keys(currentFiles).forEach(key => {
                if (key.startsWith("cells.0.")) {
                    const colId = key.split(".").pop();
                    const newKey = `cells.${tempId}.${colId}`;
                    fileDataRef.current.selectedFiles[newKey] = currentFiles[key];
                    delete fileDataRef.current.selectedFiles[key];

                    if (currentRefs[key]) {
                        currentRefs[newKey] = currentRefs[key];
                        delete currentRefs[key];
                    }
                }
            });

            const currentDeletions = fileDataRef.current.deletions;
            Object.keys(currentDeletions).forEach(key => {
                if (key.startsWith("cells.0.")) {
                    const colId = key.split(".").pop();
                    const newKey = `cells.${tempId}.${colId}`;
                    fileDataRef.current.deletions[newKey] = currentDeletions[key];
                    delete fileDataRef.current.deletions[key];
                }
            });

            const newRowCells = Object.entries(values.cells[0] || {}).map(([columnId, value]) => ({
                columnId: Number(columnId),
                rowId: tempId,
                id: 0,
                value: String(value ?? ""),
            }));

            setRows(prev => ({
                ...prev,
                [Number(table.id)]: [
                    { id: tempId, isAdded: true, tableId: table.id, cellsFk: newRowCells },
                    ...(prev[table.id] || [])
                ]
            }));

            formik.resetForm();
        }
    });
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        formik.handleSubmit();
    };
    const handleChange = (
        value:any,
        colId: number,
        rowId?: string | number
    ) => {
        const mapKey = `cells.${rowId ?? 0}.${colId}`

        formik.setFieldValue(mapKey, value);
    }; 

    useEffect(() => {
        const functionColumns = columns.filter(col => col.functionText);
        if (functionColumns.length === 0) return;

        const rowIdNum = 0;
        
        functionColumns.forEach(col => {
            const formula = col.functionText;
            if (!formula) return;

            const processedFormula = formula.replace(/\{\{(\d+)\}\}/g, (_: string, depColId: string) => {
                const val = formik.values.cells[rowIdNum]?.[Number(depColId)];
                
                if (val === undefined || val === null || val === "") return "0";
                if (typeof val === "boolean") return val ? "1" : "0";
                if (typeof val === "object") return "0";
                
                return !isNaN(Number(val)) ? String(val).replace(',', '.') : `"${val}"`;
            });

            try {
                const result = new Function(`return ${processedFormula}`)();
                const stringResult = String(result ?? "0");

                if (formik.values.cells[rowIdNum][col.id] !== stringResult) {
                    formik.setFieldValue(`cells.${rowIdNum}.${col.id}`, stringResult);
                }
            } catch (e) {
                // Formül tamamlanırken oluşabilecek hataları yutuyoruz
            }
        });
    }, [formik.values.cells, columns]);
    
    useEffect(() => {
        if (columns?.length === 0) return;

        const foreignColIds = columns
                ?.filter(col => col.realTableId != null)
                .map(col => col.realColumnId); 
        
        const loadForeignTables = async () => { 
            for (const val of tableAndRows?.data ?? []) {   
                setForeignRows(prev => {
                    const newForeignRows: typeof prev = { ...prev };
                    val?.rowsFk
                        ?.forEach(row => {
                            if (!row.cellsFk) return;
                            const filteredCells = row.cellsFk
                                        .filter(c => 
                                            foreignColIds.includes(c.columnId)
                                        );
                            if (filteredCells?.length > 0) {
                                newForeignRows[(row as any).rowId] = filteredCells;
                            }
                        });
                    return newForeignRows;
                });
            }                 
        };
        loadForeignTables();
    }, [columns]);
    return{ 
        formik, 
        foreignRows, 
        handleSubmit, 
        handleChange 
    }
}
