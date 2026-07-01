import React from "react"; 
import { TenantConfig } from "common/data/TenantTypes"; 
import useThemeMode from "hooks/useThemeMode";

interface ModulesSectionProps {
    values: TenantConfig;
    set: (k: string, v: any) => void;
}

export const ModulesSection: React.FC<ModulesSectionProps> = ({ values, set }) => {
    const { isDark } = useThemeMode(); 
        return(
        <div id="sec-moduller" className="card mb-5"> 
        </div>
    )
};