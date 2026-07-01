export const SectionLabel: React.FC<{ children: React.ReactNode; className?: string }> = ({ children, className }) => (
    <div className={`text-muted text-uppercase fw-semibold mb-2 ${className ?? ""}`} style={{ fontSize: 11, letterSpacing: ".04em" }}>
        {children}
    </div>
);
 
