import { useEffect, useRef } from "react";

export const SelectCheckbox: React.FC<{
    id: string;
    label: string;
    checked: boolean;
    exclude: boolean;
    onChange: () => void;
}> = ({ id, label, checked, exclude, onChange }) => {
    const ref = useRef<HTMLInputElement | null>(null);
    const isExcluded = checked && exclude;
    useEffect(() => {
        if (ref.current) ref.current.indeterminate = isExcluded;
    }, [isExcluded]);
    return (
        <div className="form-check">
        <input
            ref={ref}
            type="checkbox"
            className={`form-check-input${isExcluded ? " gb-exclude" : ""}`}
            id={id}
            checked={isExcluded ? false : checked}
            onChange={onChange}
        />
        <label className="form-check-label user-select-none" htmlFor={id}>{label}</label>
        </div>
    );
};
