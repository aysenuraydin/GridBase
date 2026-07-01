import React from "react";

export const ConsoleStyles: React.FC = () => (
    <style>{`
        .form-check-input.gb-exclude:indeterminate {
        background-color: var(--vz-danger) !important;
        border-color: var(--vz-danger) !important;
        background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 20 20'%3e%3cpath fill='none' stroke='%23fff' stroke-linecap='round' stroke-linejoin='round' stroke-width='3' d='M5 10h10'/%3e%3c/svg%3e") !important;
        background-repeat: no-repeat; background-position: center; background-size: contain;
        }
        .gb-console .gb-nav-item {
        border: 0; border-radius: 6px; padding: .5rem .65rem; margin-bottom: 2px;
        background: transparent; transition: background-color .12s ease;
        }
        .gb-console .gb-nav-item:hover { background: var(--vz-light); }
        .gb-console .gb-nav-item.active {
        background: rgba(var(--vz-primary-rgb), .12);
        color: var(--vz-primary); font-weight: 600;
        }
        .gb-console .gb-nav-item.active .gb-ep-label { color: var(--vz-primary); }
        .gb-console .gb-url-bar {
        background: var(--vz-light); border-radius: 8px; padding: .5rem .75rem;
        font-family: var(--bs-font-monospace); font-size: 12.5px;
        }
        .gb-console .gb-section + .gb-section { margin-top: 1.25rem; padding-top: 1.25rem; border-top: 1px solid var(--vz-border-color); }
        .gb-console .gb-response { font-family: var(--bs-font-monospace); font-size: 12px; line-height: 1.6; }
        .gb-console .gb-method-pill { min-width: 56px; text-align: center; letter-spacing: .03em; }
        .gb-console code { color: var(--vz-primary); }
        .gb-console .gb-row-tools { opacity: 0; transition: opacity .15s; }
        .gb-console .gb-nav-item:hover .gb-row-tools { opacity: 1; }
    `}</style>
);