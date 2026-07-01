import { DataType } from 'common/enums/DataType';
import { ModalType } from 'common/enums/ModalType';
import { toSafeId } from 'common/utils/stringUtils';
import { PopConfirm } from 'components/Common/PopConfirm';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import { useDatatableAction } from '../hooks/useDatatableAction'; 
import { useCanWrite } from 'hooks/useCanWrite';

interface ActionsProps {
    handleClick: (arg: any, type: DataType) => void;
    tableId: number; 
    props: any; 
    hardDelete?: (id: number) => void; 
}

export const Actions = ({ handleClick, props, tableId }: ActionsProps) => { 
        
    const safeName = toSafeId([props?.name, props?.id], "q", "action"); 
    const { deleteItem } = useDatatableAction(tableId);  
    const canWrite = useCanWrite(tableId); 
    
    return (
        <div className="d-flex gap-2 justify-content-end me-1">
        <ul className="list-inline hstack gap-2 mb-0">
                    <li className="list-inline-item">
                        <Link to="#" className="text-primary d-inline-block" onClick={(e) => {
                            e.preventDefault(); 
                            e.stopPropagation();
                            handleClick(props, DataType.View);
                        }}>
                            <i className="ri-eye-fill fs-16"></i>
                        </Link>
                    </li>
                    {canWrite &&
                    <>
                        <li className="list-inline-item edit">
                            <Link to="#" className="text-primary d-inline-block edit-item-btn" onClick={() => handleClick(props, DataType.Edit)}>
                                <i className="ri-pencil-fill fs-16"></i>
                            </Link>
                        </li>
                        <li className="list-inline-item">
                            <Link to="#" id={`q-${safeName}-col-popconfirm-${props?.id}`} className="btn btn-sm btn-soft-danger btn-hover">
                                <i className="ri-delete-bin-5-fill fs-14 text-danger"></i>
                            </Link> 
                            <PopConfirm 
                                targetId={`q-${safeName}-col-popconfirm-${props?.id}`} 
                                type={ModalType.Alert}
                                message='Bu kaydı silmek istediğinizden emin misiniz?'
                                confirmText='Sil!'
                                onConfirm={async () => await deleteItem(props?.id)} 
                                onClose={() => toast.error("Silme işlemi iptal edildi!")} 
                            />
                        </li> 
                    </>
                    }
                </ul> 
        <style>
        {`
            .hoverColor:hover i { color: white !important; }
            .btn-hover:hover { color: white !important; }
            .btn-hover:hover i { color: white !important; }
        `}
        </style>
        </div>
    );
};