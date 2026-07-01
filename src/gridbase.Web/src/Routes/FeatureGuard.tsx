import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from 'context/AuthContext';
import { useUserProfile } from 'hooks/useUser';

interface GuardProps {
    children?: React.ReactNode;
    allowedRoles?: string[];
}

export const FeatureGuard = ({ children, allowedRoles }: GuardProps) => {
    const { user: usr } = useAuth();
    const { data: user, isLoading: isUserLoading } = useUserProfile(usr?.id ?? "");

    if (isUserLoading) {
        return <div>Yükleniyor...</div>;
    }

    if (!children) {
        return <Navigate to="/dashboard" replace />;
    }

    if (allowedRoles && allowedRoles.length > 0) {
        const hasRole = user?.roles?.some((role: string) => allowedRoles.includes(role));
        if (!hasRole) {
            return <Navigate to="/dashboard" replace />;
        }
    }

    return <>{children}</>;
};