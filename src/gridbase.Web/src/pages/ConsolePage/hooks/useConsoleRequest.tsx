import { useState } from "react";
import { api } from "helpers/backend_helper"; 
import { EndpointDef } from "../components/console.types";
import { useQueryClient } from "@tanstack/react-query";
export const useConsoleRequest = () => {
    const [respStatus, setRespStatus] = useState<number | null>(null);
    const [respBody, setRespBody] = useState("");
    const [respTime, setRespTime] = useState<number | null>(null);
    const [sending, setSending] = useState(false);
    const qc = useQueryClient();

    const safeParse = (txt: string) => { try { return JSON.parse(txt); } catch { return {}; } };

    const send = async (endpoint: EndpointDef, fullUrl: string, bodyText: string) => {
        setSending(true); setRespStatus(null); setRespBody(""); setRespTime(null);
        const t0 = performance.now();
        try {
        let res: any;
        const m = endpoint.method;
        if (m === "GET") res = await api.get(fullUrl);
        else if (m === "POST") res = await api.create(fullUrl, safeParse(bodyText));
        else if (m === "PUT") res = await api.put(fullUrl, safeParse(bodyText));
        else if (m === "PATCH") res = await api.patch(fullUrl, safeParse(bodyText));
        else if (m === "DELETE") res = await api.delete(fullUrl);

        setRespTime(Math.round(performance.now() - t0));

        const hasHttpStatus = res && typeof res === "object" && typeof res.status === "number";
        const payload = hasHttpStatus && "data" in res ? res.data : res;

        const isEmpty =
            payload === null || payload === undefined || payload === "" ||
            (typeof payload === "object" && !Array.isArray(payload) && Object.keys(payload).length === 0);

        let status: number;
        if (hasHttpStatus) status = res.status;
        else if (m === "DELETE") status = 204;
        else if (isEmpty) status = 204;
        else status = m === "POST" ? 201 : 200;

        setRespStatus(status);
        if (isEmpty) {
            setRespBody(status === 204
            ? "204 No Content — islem basarili, govde dondurulmedi."
            : "(bos cevap)");
        } else {
            setRespBody(JSON.stringify(payload, null, 2));
        }
        } catch (err: any) {
        setRespTime(Math.round(performance.now() - t0));
        const resp = err?.response;
        const status = resp?.status ?? err?.status ?? 0;
        const data = resp?.data ?? err?.message ?? String(err);
        setRespStatus(status);

        let bodyOut: string;
        if (typeof data === "string") {
            bodyOut = data;
        } else if (data && typeof data === "object") {
            const msg = data.message || data.title || "";
            const errs = data.errors
            ? "\n\nDetay:\n" + Object.entries(data.errors)
                .map(([k, v]) => `  ${k}: ${(Array.isArray(v) ? v.join("; ") : v)}`)
                .join("\n")
            : "";
            const head = status === 0 ? "Sunucuya ulasilamadi / CORS / sunucu kapali olabilir." : `${status}`;
            bodyOut = (msg ? `${head} — ${msg}${errs}\n\n` : `${head}\n\n`) + JSON.stringify(data, null, 2);
        } else {
            bodyOut = String(data);
        }
        setRespBody(bodyOut);
        } finally {
        setSending(false);
        }
    };

    return { respStatus, respBody, respTime, sending, send };
};