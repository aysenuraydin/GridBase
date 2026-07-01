import { Datatable } from 'common/data/Datatable';
import { DataType } from 'common/enums/DataType';
import { ModalType } from 'common/enums/ModalType';
import { toSafeId } from 'common/utils/stringUtils';
import { PopConfirm } from 'components/Common/PopConfirm';
import { Dispatch, SetStateAction } from 'react';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import { DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from 'reactstrap';
import { TabItem } from '../hooks/useTabState'; 
import { useAuth } from 'context/AuthContext';
import { useUserProfile } from 'hooks/useUser';

interface ActionsProps {
    handleTableClick: (arg: any, type: DataType) => void;
    handleEditColumnsClick: (arg?: Datatable | undefined) => void;
    handleRowsClick: (arg?: Datatable | undefined) => void;
    handleRelationClick: (arg?: Datatable | undefined) => void;
    handleDesignClick?: (arg?: Datatable | undefined) => void;
    handleSettingClick?: (arg?: Datatable | undefined) => void; 
    cellProps: any;
    toggleTab: (t: { name: string; id: number; }) => void;
    setTabs: Dispatch<SetStateAction<TabItem[]>>;
    deleteDatatable: (id: number) => void;
    backToDelete?: (id: number) => void;
    hardDelete?: (id: number) => void; 
    tableName: string;
}

export const Actions = ({
    handleTableClick,
    handleEditColumnsClick,
    handleRowsClick,
    handleRelationClick,
    handleSettingClick,
    cellProps,
    toggleTab, 
    setTabs, 
    deleteDatatable,
    backToDelete,
    hardDelete, 
    tableName
}: ActionsProps) => { 
    const getSafeName = (name?: string) => toSafeId([name], "table", "data");
    const { user: usr } = useAuth(); 
    const { data: user } = useUserProfile(usr?.id ?? "");
    const isAdmin = user?.roles?.includes("GB"); 

    return (
    <>
        <div className="d-flex gap-2 justify-content-end me-1">
            <ul className="list-inline hstack gap-2 mb-0">
                <li className="list-inline-item"> 
                    <Link to={`/console?table=${tableName}`}>⚡</Link>
                </li>
                <li className="list-inline-item">
                    <Link to="#" className="text-primary d-inline-block text-decoration-none" onClick={() => handleTableClick(cellProps.row.original, DataType.View)}>
                        <i className="ri-eye-fill fs-16"></i>
                    </Link>
                </li>
                <li className="list-inline-item edit">
                    <Link to="#" className="text-primary d-inline-block edit-item-btn text-decoration-none" onClick={() => handleTableClick(cellProps.row.original, DataType.Edit)}>
                        <i className="ri-pencil-fill fs-16"></i>
                    </Link>
                </li>
                <li className="list-inline-item">
                    <Link to="#" className={`text-primary d-inline-block edit-item-btn text-decoration-none`} onClick={() => isAdmin && handleSettingClick?.(cellProps.row.original)}>
                        <i className="ri-settings-5-fill fs-16"></i>
                    </Link>
                </li>
                <li className="list-inline-item">
                    <div id={`table-popconfirm-${cellProps.row.original.id}`} className={`btn btn-sm btn-soft-danger btn-hover hoverColor`}>
                        <i className={`ri-delete-bin-5-fill fs-14 text-danger`}></i>
                    </div>
                    <PopConfirm 
                        targetId={`table-popconfirm-${cellProps.row.original.id}`}
                        type={ModalType.Alert}
                        message='Bu kaydı silmek istediğinizden emin misiniz?'
                        confirmText='Sil!'
                        onConfirm={async () => await deleteDatatable(cellProps.row.original.id)} 
                        onClose={() => toast.error("Silme işlemi iptal edildi!")} 
                    />
                </li>
            </ul>
            <UncontrolledDropdown>
                <DropdownToggle tag="a" className={`btn btn-soft-primary btn-sm`}>
                    <i className="ri-more-fill align-middle"></i>
                </DropdownToggle>
                { isAdmin && (
                    <DropdownMenu className="dropdown-menu-end">
                        <li>
                            <DropdownItem className="edit-item-btn" onClick={() => handleEditColumnsClick(cellProps.row.original)}>
                                <i className="ri-pencil-fill align-bottom me-2 text-muted"></i> Sütunları Düzenle
                            </DropdownItem>
                        </li>
                        <li>
                            <DropdownItem className="edit-item-btn" onClick={() => {
                                setTabs((prev: TabItem[]) => {
                                    if (prev.some(tab => tab.id === cellProps.row.original?.id)) return prev;
                                    return [...prev, { name: cellProps.row.original?.name ?? "", id: cellProps.row.original?.id }];
                                });
                                toggleTab({ name: cellProps.row.original?.name ?? "", id: cellProps.row.original?.id });
                                handleRowsClick(cellProps.row.original)
                            }}>
                                <i className="ri-pencil-fill align-bottom me-2 text-muted"></i> Satırları Düzenle
                            </DropdownItem>
                        </li> 
                        <li>
                            <DropdownItem className="edit-item-btn" onClick={() => handleRelationClick(cellProps.row.original)}>
                                <i className="ri-pencil-fill align-bottom me-2 text-muted"></i> İlişkileri Düzenle
                            </DropdownItem>
                        </li>
                    </DropdownMenu>
                )}
            </UncontrolledDropdown>
        </div>
        <style>{`
            .hoverColor:hover i { color: white !important; }
        `}</style>
    </>
    );
};