import { useRef, useState, useEffect } from "react";
import { EndpointDef, FilterRow } from "../components/console.types";

/**
 * useQueryBuilder — GridBase API Console'un sorgu motoru (frontend tarafı).
 *
 *  NE YAPAR (özet):
 *  GridBase'in kendi query dili ile görsel sorgu kurucu arasında ÇİFT YÖNLÜ
 *  senkronizasyon sağlar. Kullanıcı ister form kontrollerini (filtre/sıralama/
 *  alan seçimi/arama/ilişki genişletme/sayfalama) kullanır, ister ham query
 *  string'i elle yazar — ikisi her an birbirine dönüştürülür.
 *
 *  - State → query string:  buildQueryString()
 *      filter=col:op:val (isnull/isnotnull değersiz), sort=col:dir,
 *      select=a,b / select=-a,-b (include/exclude), search=..&searchFields=..,
 *      expand=relA,relB, page/size (yalnızca 'paged' uç noktasında).
 *      URL-encode + boş alan elemesi + mod'a göre '-' prefix üretimi.
 *
 *  - Query string → state:  parseQueryToControls()
 *      Ham string'i ayrıştırıp tüm kontrolleri geri doldurur; select'te
 *      hepsi '-' ile başlıyorsa exclude moduna geçer; filter'da col:op:val
 *      üçlüsünü (değerde ':' olabileceğini hesaba katarak) çözer.
 *
 *  - Sonsuz döngü koruması:  editingRaw ref'i ile "state→raw" ve "raw→state"
 *      yönlerinin birbirini tetiklemesi engellenir.
 *
 *  Bu dosya, GridBase sorgu sözleşmesinin istemci karşılığıdır
 */

export interface QueryBuilder {
    filters: FilterRow[];
    sortCol: string;
    sortDir: "asc" | "desc";
    selectCols: string[];
    selectMode: "include" | "exclude";
    searchText: string;
    searchFields: string[];
    expandCols: string[];
    page: string;
    size: string;
    rawQuery: string;
    setSortCol: (v: string) => void;
    setSortDir: (v: "asc" | "desc") => void;
    setSelectMode: (v: "include" | "exclude") => void;
    setSearchText: (v: string) => void;
    setSearchFields: (updater: (s: string[]) => string[]) => void;
    setPage: (v: string) => void;
    setSize: (v: string) => void;
    reset: () => void;
    onRawQueryChange: (value: string) => void;
    addFilter: (firstCol: string) => void;
    updateFilter: (i: number, patch: Partial<FilterRow>) => void;
    removeFilter: (i: number) => void;
    toggleSelect: (key: string) => void;
    toggleExpand: (name: string) => void;
}

export const useQueryBuilder = (selectedEndpoint: EndpointDef | null): QueryBuilder => {
    //  Görsel sorgu kurucu ↔ GridBase query dili çift yönlü senkron motoru.
    //  (Gövde gizlendi — yukarıdaki açıklama bloğuna bakınız.)
    throw new Error("Source available on request.");
};