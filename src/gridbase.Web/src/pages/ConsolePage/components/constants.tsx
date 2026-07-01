import { EndpointDef, Method } from "./console.types";

export const toCamelTable = (name: string | null | undefined): string => {
    if (!name) return "";
    const compact = name.replace(/\s+/g, "");
    return compact.charAt(0).toLowerCase() + compact.slice(1);
};


export const buildEndpoints = (table: string): EndpointDef[] => [
    { id: "list",   method: "GET",    path: `/gridbase/${table}`, label: "Listele", needsId: false, needsBody: false, supportsQuery: true },
    { id: "one",    method: "GET",    path: `/gridbase/${table}/one`, label: "Tek kayit (filtre)", needsId: false, needsBody: false, supportsQuery: true },
    { id: "paged",  method: "GET",    path: `/gridbase/${table}/paged`, label: "Sayfali", needsId: false, needsBody: false, supportsQuery: true },
    { id: "byId",   method: "GET",    path: `/gridbase/${table}/{id}`, label: "Id ile getir",       needsId: true,  needsBody: false, supportsQuery: true },
    { id: "create", method: "POST",   path: `/gridbase/${table}`, label: "Olustur", needsId: false, needsBody: true,  supportsQuery: false },
    { id: "update", method: "PUT",    path: `/gridbase/${table}/{id}`, label: "Guncelle (tam)",     needsId: true,  needsBody: true,  supportsQuery: false },
    { id: "patch",  method: "PATCH",  path: `/gridbase/${table}/{id}`, label: "Guncelle (kismi)",   needsId: true,  needsBody: true,  supportsQuery: false },
    { id: "delete", method: "DELETE", path: `/gridbase/${table}/{id}`, label: "Sil", needsId: true,  needsBody: false, supportsQuery: false },
    { id: "delcol",    method: "DELETE", path: `/gridbase/${table}/columns/{columnName}`, label: "Kolon sil", needsId: false, needsBody: false, supportsQuery: false },

    { id: "relList",   method: "GET",    path: `/gridbase/relations/${table}`, label: "İlişkileri listele", needsId: false, needsBody: false, supportsQuery: false },
    { id: "relCreate", method: "POST",   path: `/gridbase/relations/${table}`, label: "İlişki kur",         needsId: false, needsBody: true,  supportsQuery: false },

    // ── VALIDATION (kolon kural ayarlama) ──
    { id: "valGet", method: "GET", path: `/gridbase/${table}/columns/{columnName}/validation`, label: "Kuralları getir", needsId: false, needsBody: false, supportsQuery: false },
    { id: "valSet", method: "PUT", path: `/gridbase/${table}/columns/{columnName}/validation`, label: "Kural ayarla",    needsId: false, needsBody: true,  supportsQuery: false },
];

export const RELATION_BODY_TEMPLATE = JSON.stringify(
    { toTable: "", isMultiSelect: false },
    null,
    2
);

// Validation kural ayarlama için hazır şablon (string enum'larla)
export const VALIDATION_BODY_TEMPLATE = JSON.stringify(
    {
        type: "text",
        rules: [
            { rule: "required", value: "", message: "" }
        ]
    },
    null,
    2
);

export const ALL_OPS = ["eq", "neq", "contains", "startswith", "endswith", "gt", "gte", "lt", "lte", "in", "isnull", "isnotnull"];
export const numericTypes = ["Number", "Range", "Ratings", "Progress"];
export const textTypes = ["Text", "Textarea", "Email", "Url", "Tel", "Password", "Color", "Select", "Radio"];

export const opsForType = (type: string): string[] => {
    if (numericTypes.includes(type)) return ["eq", "neq", "gt", "gte", "lt", "lte", "in", "isnull", "isnotnull"];
        if (type === "Date" || type === "DatetimeLocal") return ["eq", "neq", "gt", "gte", "lt", "lte", "isnull", "isnotnull"];
        if (textTypes.includes(type)) return ["contains", "startswith", "endswith", "eq", "neq", "in", "isnull", "isnotnull"];
        return ALL_OPS;
};

export const methodColor: Record<Method, string> = {
    GET: "success", POST: "primary", PUT: "warning", PATCH: "info", DELETE: "danger",
};

// status → renk + etiket
export const statusInfo = (s: number | null): { color: string; text: string } => {
    if (s === null) return { color: "secondary", text: "" };
    if (s === 0) return { color: "danger", text: "Baglanti hatasi" };
    if (s >= 200 && s < 300) {
        const label = s === 201 ? "201 Created" : s === 204 ? "204 No Content" : `${s} OK`;
        return { color: "success", text: label };
    }
    if (s >= 400 && s < 500) {
        const label = s === 400 ? "400 Bad Request"
        : s === 401 ? "401 Unauthorized"
        : s === 403 ? "403 Forbidden"
        : s === 404 ? "404 Not Found"
        : s === 409 ? "409 Conflict"
        : `${s}`;
        return { color: "warning", text: label };
    }
    if (s >= 500) return { color: "danger", text: `${s} Server Error` };
    return { color: "secondary", text: `${s}` };
};

export const toFieldType = (schemaType: string): string => {
    const t = (schemaType || "").toLowerCase();
    if (["number", "range", "ratings", "progress"].includes(t)) return "number";
    if (["boolean", "checkbox", "switch"].includes(t)) return "boolean";
    if (["date", "datetime", "datetimelocal"].includes(t)) return "date";
    if (["badges", "dropfiles", "multipledate", "multipletime"].includes(t)) return "array";
    return "text";
};

// her field type için önerilen kurallar (modal'daki forTypes mantığı)
export const RECOMMENDED_RULES: Record<string, string[]> = {
    text:    ["required", "minLength", "maxLength", "email", "pattern", "unique"],
    number:  ["required", "positive", "min", "max", "integer"],
    boolean: ["required"],
    date:    ["required"],
    array:   ["required"],
    mixed:   ["required"],
};

// kolon + tip → hazır validation JSON
export const buildValidationTemplate = (fieldType: string): string => {
    const ruleNames = RECOMMENDED_RULES[fieldType] ?? ["required"];
    return JSON.stringify(
        {
        type: fieldType,
        rules: ruleNames.map((rule) => ({ rule, value: null, message: "" })),
        },
        null,
        2
    );
};