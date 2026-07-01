import React from "react";
import { Card, CardBody, CardHeader, Badge } from "reactstrap";
import { statusInfo } from "./constants";

export const ResponsePanel: React.FC<{
    respStatus: number | null;
    respBody: string;
    respTime: number | null;
}> = ({ respStatus, respBody, respTime }) => {
    const sInfo = statusInfo(respStatus);

    return (
        <Card className="h-100 shadow-none border">
        <CardHeader className="bg-transparent border-bottom py-3 d-flex align-items-center gap-2">
            <i className="ri-arrow-left-right-line text-muted"></i>
            <span className="fw-semibold">Yanit</span>
            {respStatus !== null && <Badge color={sInfo.color} className="ms-1">{sInfo.text}</Badge>}
            {respTime !== null && (
            <small className="text-muted ms-auto">
                <i className="ri-time-line me-1"></i>{respTime} ms
            </small>
            )}
        </CardHeader>
        <CardBody className="p-0">
            {respBody ? (
            <pre className="gb-response mb-0 p-3"
                style={{ maxHeight: "72vh", overflow: "auto", whiteSpace: "pre-wrap", wordBreak: "break-word" }}>
                {respBody}
            </pre>
            ) : (
            <div className="d-flex flex-column align-items-center justify-content-center text-center text-muted py-5">
                <i className="ri-inbox-line display-6 opacity-25 mb-2"></i>
                <p className="small mb-0">Istegi gonder, yanit burada gorunsun.</p>
            </div>
            )}
        </CardBody>
        </Card>
    );
};