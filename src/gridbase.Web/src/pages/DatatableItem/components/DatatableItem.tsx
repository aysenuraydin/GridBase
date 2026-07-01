import { Card, CardBody, Container, CardHeader, Alert } from "reactstrap"; 
import BreadCrumb from "components/Common/BreadCrumb";
import DeleteModal from "components/Common/DeleteModal";
import { toast, ToastContainer } from 'react-toastify';
import ExportCSVModal from "components/Common/ExportCSVModal"; 
import { TableHeader } from "./TableHeader";
import TableContainer from "components/Common/TableContainer"; 
import { TableViewType } from "common/enums/TableViewType";
import { CreateRowModal } from "../CreateRow/components/CreateRowModal";  
import { useDataTableItem } from "context/DatatableItemContext";
import { TableColumn } from "common/data/TableColumn";
import 'react-toastify/dist/ReactToastify.css';
import "../DatatableItem.css"
import { useGetBrand } from "hooks/useBrand";
import useThemeMode from "hooks/useThemeMode";

export const DatatableItem = () => {
  const { data:brand } = useGetBrand();
  const { isDark } = useThemeMode(); 
  const{  
      isExportCSV, setIsExportCSV, 
      deleteModalMulti, deleteTableMultiple,
      setTableDeleteModalMulti,deleteCheckedRow,
      dynamicGlobalStyles, tableData,
      cols, 
      columns, 
      deletedRows, 
      table,    
  } = useDataTableItem();  

  document.title = (table?.name ?? "Table") +" | " +(brand?.companyName || "Gridbase");

  return ( 
    <div className="page-content" style={{userSelect:"none"}}>
      <style dangerouslySetInnerHTML={{ __html: dynamicGlobalStyles || "" }} /> 
      <ExportCSVModal
        show={isExportCSV}
        onCloseClick={() => setIsExportCSV(false)}
        data={table}
      />
      <DeleteModal
        show={deleteModalMulti}
        onDeleteClick={() => {
          deleteTableMultiple();
          setTableDeleteModalMulti(false);
          deleteCheckedRow()
        }}
        onCloseClick={() => {
          setTableDeleteModalMulti(false);
          toast.error("Hiçbir tablo silinemedi!");
        }}
      />

      <Container fluid>
        <BreadCrumb title="Tablo" pageTitle={brand?.companyName || "Gridbase"} /> 
          <Card style={{position:"relative"}}>
              <CardHeader className="card-header btable-0">
                <TableHeader  />
              </CardHeader>
              <CardBody className="pt-0" style={{minHeight:"65vh"}}> 
                <TableContainer
                    columns={cols ?? []}              
                    data={tableData ?? []}  
                    rows={ deletedRows ?? []}  
                    isGlobalFilter
                    customPageSize={table?.pageSize ?? 8}
                    SearchPlaceholder="Search for table Id, name, table status or something..."
                    divClass="table-responsive table-card mb-1 pt-0 table-min-height"
                    tableClass="align-middle table-nowrap"
                    filterColumns={(columns ?? []).filter((x:TableColumn) => x.isFilter)}
                    isIcon = { table?.viewType == TableViewType.Grid }
                    theadClass={`table-${isDark ? 'dark':'light'} text-muted text-uppercase`}
                    thClass={`${isDark ? 'text-light':'text-dark'}`} 
                    tbl={table}
                /> 
                <CreateRowModal/>
                <ToastContainer closeButton={true} limit={3} style={{marginTop:"100px"}}/>
              </CardBody>
          </Card>
      </Container> 
    </div>
  );
};  

