package io.constructor.core;

import java.lang.System;

@android.annotation.SuppressLint(value = {"StaticFieldLeak"})
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000\u009c\u0001\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0005\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0002\u0010\u000e\n\u0002\u0010\u0002\n\u0002\b\b\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0003\n\u0002\u0010 \n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\b\n\u0002\b\u0007\n\u0002\u0018\u0002\n\u0002\b\u0004\n\u0002\u0018\u0002\n\u0002\b\u0007\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u000e\n\u0002\u0010\u0006\n\u0002\b\t\n\u0002\u0010\u0011\n\u0002\b\u0012\b\u00c7\u0002\u0018\u00002\u00020\u0001B\u0007\b\u0002\u00a2\u0006\u0002\u0010\u0002J\u0006\u0010\u001d\u001a\u00020\u0016J\u001a\u0010\u001e\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020!0 0\u001f2\u0006\u0010\"\u001a\u00020\u0015J\u0087\u0001\u0010#\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020$0 0\u001f2\u0006\u0010%\u001a\u00020\u00152\u0006\u0010&\u001a\u00020\u00152\"\b\u0002\u0010\'\u001a\u001c\u0012\u0016\u0012\u0014\u0012\u0004\u0012\u00020\u0015\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u00150(0)\u0018\u00010(2\n\b\u0002\u0010*\u001a\u0004\u0018\u00010+2\n\b\u0002\u0010,\u001a\u0004\u0018\u00010+2\n\b\u0002\u0010-\u001a\u0004\u0018\u00010+2\n\b\u0002\u0010.\u001a\u0004\u0018\u00010\u00152\n\b\u0002\u0010/\u001a\u0004\u0018\u00010\u0015\u00a2\u0006\u0002\u00100J\u0006\u00101\u001a\u00020\u0015J\u007f\u00102\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u0002030 0\u001f2\u0006\u0010\"\u001a\u00020\u00152\"\b\u0002\u0010\'\u001a\u001c\u0012\u0016\u0012\u0014\u0012\u0004\u0012\u00020\u0015\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u00150(0)\u0018\u00010(2\n\b\u0002\u0010*\u001a\u0004\u0018\u00010+2\n\b\u0002\u0010,\u001a\u0004\u0018\u00010+2\n\b\u0002\u0010-\u001a\u0004\u0018\u00010+2\n\b\u0002\u0010.\u001a\u0004\u0018\u00010\u00152\n\b\u0002\u0010/\u001a\u0004\u0018\u00010\u0015\u00a2\u0006\u0002\u00104J\u0006\u00105\u001a\u00020+J\u0018\u00106\u001a\u00020\u00162\b\u0010\u000b\u001a\u0004\u0018\u00010\f2\u0006\u00107\u001a\u000208J7\u00109\u001a\u00020\u00162\b\u0010\u000b\u001a\u0004\u0018\u00010\f2\u0006\u00107\u001a\u0002082\u0006\u0010\r\u001a\u00020\u000e2\u0006\u0010\u0011\u001a\u00020\u00122\u0006\u0010\t\u001a\u00020\nH\u0000\u00a2\u0006\u0002\b:J6\u0010;\u001a\u00020\u00162\u0006\u0010<\u001a\u00020\u00152\u0006\u0010=\u001a\u00020\u00152\u0006\u0010>\u001a\u00020\u00152\n\b\u0002\u0010?\u001a\u0004\u0018\u00010@2\n\b\u0002\u0010A\u001a\u0004\u0018\u00010\u0015J=\u0010B\u001a\u00020C2\u0006\u0010<\u001a\u00020\u00152\u0006\u0010=\u001a\u00020\u00152\u0006\u0010>\u001a\u00020\u00152\n\b\u0002\u0010?\u001a\u0004\u0018\u00010@2\n\b\u0002\u0010A\u001a\u0004\u0018\u00010\u0015H\u0000\u00a2\u0006\u0002\bDJ>\u0010E\u001a\u00020\u00162\u0006\u0010%\u001a\u00020\u00152\u0006\u0010&\u001a\u00020\u00152\u0006\u0010F\u001a\u00020\u00152\u0006\u0010G\u001a\u00020+2\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u00152\n\b\u0002\u0010A\u001a\u0004\u0018\u00010\u0015JE\u0010H\u001a\u00020C2\u0006\u0010%\u001a\u00020\u00152\u0006\u0010&\u001a\u00020\u00152\u0006\u0010F\u001a\u00020\u00152\u0006\u0010G\u001a\u00020+2\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u00152\n\b\u0002\u0010A\u001a\u0004\u0018\u00010\u0015H\u0000\u00a2\u0006\u0002\bIJ(\u0010J\u001a\u00020\u00162\u0006\u0010%\u001a\u00020\u00152\u0006\u0010&\u001a\u00020\u00152\u0006\u0010K\u001a\u00020+2\b\b\u0002\u0010L\u001a\u00020\u0015J/\u0010M\u001a\u00020C2\u0006\u0010%\u001a\u00020\u00152\u0006\u0010&\u001a\u00020\u00152\u0006\u0010K\u001a\u00020+2\b\b\u0002\u0010L\u001a\u00020\u0015H\u0000\u00a2\u0006\u0002\bNJ;\u0010O\u001a\u00020\u00162\u0006\u0010P\u001a\u00020\u00152\u0006\u0010F\u001a\u00020\u00152\b\u0010Q\u001a\u0004\u0018\u00010R2\b\b\u0002\u0010<\u001a\u00020\u00152\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u0015\u00a2\u0006\u0002\u0010SJ?\u0010T\u001a\u00020C2\u0006\u0010P\u001a\u00020\u00152\u0006\u0010F\u001a\u00020\u00152\b\u0010Q\u001a\u0004\u0018\u00010R2\b\b\u0002\u0010<\u001a\u00020\u00152\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u0015H\u0000\u00a2\u0006\u0004\bU\u0010VJ\u0010\u0010W\u001a\u00020\u00162\b\u0010\"\u001a\u0004\u0018\u00010\u0015J\u0017\u0010X\u001a\u00020C2\b\u0010\"\u001a\u0004\u0018\u00010\u0015H\u0000\u00a2\u0006\u0002\bYJ7\u0010Z\u001a\u00020\u00162\f\u0010[\u001a\b\u0012\u0004\u0012\u00020\u00150\\2\b\u0010Q\u001a\u0004\u0018\u00010R2\u0006\u0010]\u001a\u00020\u00152\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u0015\u00a2\u0006\u0002\u0010^J;\u0010_\u001a\u00020C2\f\u0010[\u001a\b\u0012\u0004\u0012\u00020\u00150\\2\b\u0010Q\u001a\u0004\u0018\u00010R2\u0006\u0010]\u001a\u00020\u00152\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u0015H\u0000\u00a2\u0006\u0004\b`\u0010aJ8\u0010b\u001a\u00020\u00162\u0006\u0010P\u001a\u00020\u00152\u0006\u0010F\u001a\u00020\u00152\b\b\u0002\u0010<\u001a\u00020\u00152\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u00152\n\b\u0002\u0010A\u001a\u0004\u0018\u00010\u0015J?\u0010c\u001a\u00020C2\u0006\u0010P\u001a\u00020\u00152\u0006\u0010F\u001a\u00020\u00152\b\b\u0002\u0010<\u001a\u00020\u00152\n\b\u0002\u0010>\u001a\u0004\u0018\u00010\u00152\n\b\u0002\u0010A\u001a\u0004\u0018\u00010\u0015H\u0000\u00a2\u0006\u0002\bdJ\u0016\u0010e\u001a\u00020\u00162\u0006\u0010\"\u001a\u00020\u00152\u0006\u0010K\u001a\u00020+J\u001d\u0010f\u001a\u00020C2\u0006\u0010\"\u001a\u00020\u00152\u0006\u0010K\u001a\u00020+H\u0000\u00a2\u0006\u0002\bgJ \u0010h\u001a\u00020\u00162\u0006\u0010<\u001a\u00020\u00152\u0006\u0010=\u001a\u00020\u00152\b\u0010?\u001a\u0004\u0018\u00010@J\'\u0010i\u001a\u00020C2\u0006\u0010<\u001a\u00020\u00152\u0006\u0010=\u001a\u00020\u00152\b\u0010?\u001a\u0004\u0018\u00010@H\u0000\u00a2\u0006\u0002\bjJ\b\u0010k\u001a\u00020\u0016H\u0002J\r\u0010l\u001a\u00020CH\u0000\u00a2\u0006\u0002\bmR\u001b\u0010\u0003\u001a\u00020\u00048@X\u0080\u0084\u0002\u00a2\u0006\f\n\u0004\b\u0007\u0010\b\u001a\u0004\b\u0005\u0010\u0006R\u000e\u0010\t\u001a\u00020\nX\u0082.\u00a2\u0006\u0002\n\u0000R\u000e\u0010\u000b\u001a\u00020\fX\u0082.\u00a2\u0006\u0002\n\u0000R\u000e\u0010\r\u001a\u00020\u000eX\u0082.\u00a2\u0006\u0002\n\u0000R\u000e\u0010\u000f\u001a\u00020\u0010X\u0082\u000e\u00a2\u0006\u0002\n\u0000R\u000e\u0010\u0011\u001a\u00020\u0012X\u0082.\u00a2\u0006\u0002\n\u0000R\u001a\u0010\u0013\u001a\u000e\u0012\u0004\u0012\u00020\u0015\u0012\u0004\u0012\u00020\u00160\u0014X\u0082\u000e\u00a2\u0006\u0002\n\u0000R(\u0010\u0018\u001a\u0004\u0018\u00010\u00152\b\u0010\u0017\u001a\u0004\u0018\u00010\u00158F@FX\u0086\u000e\u00a2\u0006\f\u001a\u0004\b\u0019\u0010\u001a\"\u0004\b\u001b\u0010\u001c\u00a8\u0006n"}, d2 = {"Lio/constructor/core/ConstructorIo;", "", "()V", "component", "Lio/constructor/injection/component/AppComponent;", "getComponent$library_debug", "()Lio/constructor/injection/component/AppComponent;", "component$delegate", "Lkotlin/Lazy;", "configMemoryHolder", "Lio/constructor/data/memory/ConfigMemoryHolder;", "context", "Landroid/content/Context;", "dataManager", "Lio/constructor/data/DataManager;", "disposable", "Lio/reactivex/disposables/CompositeDisposable;", "preferenceHelper", "Lio/constructor/data/local/PreferencesHelper;", "sessionIncrementHandler", "Lkotlin/Function1;", "", "", "value", "userId", "getUserId", "()Ljava/lang/String;", "setUserId", "(Ljava/lang/String;)V", "appMovedToForeground", "getAutocompleteResults", "Lio/reactivex/Observable;", "Lio/constructor/data/ConstructorData;", "Lio/constructor/data/model/autocomplete/AutocompleteResponse;", "term", "getBrowseResults", "Lio/constructor/data/model/browse/BrowseResponse;", "filterName", "filterValue", "facets", "", "Lkotlin/Pair;", "page", "", "perPage", "groupId", "sortBy", "sortOrder", "(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;Ljava/lang/Integer;Ljava/lang/Integer;Ljava/lang/Integer;Ljava/lang/String;Ljava/lang/String;)Lio/reactivex/Observable;", "getClientId", "getSearchResults", "Lio/constructor/data/model/search/SearchResponse;", "(Ljava/lang/String;Ljava/util/List;Ljava/lang/Integer;Ljava/lang/Integer;Ljava/lang/Integer;Ljava/lang/String;Ljava/lang/String;)Lio/reactivex/Observable;", "getSessionId", "init", "constructorIoConfig", "Lio/constructor/core/ConstructorIoConfig;", "testInit", "testInit$library_debug", "trackAutocompleteSelect", "searchTerm", "originalQuery", "sectionName", "resultGroup", "Lio/constructor/data/model/common/ResultGroup;", "resultID", "trackAutocompleteSelectInternal", "Lio/reactivex/Completable;", "trackAutocompleteSelectInternal$library_debug", "trackBrowseResultClick", "customerId", "resultPositionOnPage", "trackBrowseResultClickInternal", "trackBrowseResultClickInternal$library_debug", "trackBrowseResultsLoaded", "resultCount", "url", "trackBrowseResultsLoadedInternal", "trackBrowseResultsLoadedInternal$library_debug", "trackConversion", "itemName", "revenue", "", "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Double;Ljava/lang/String;Ljava/lang/String;)V", "trackConversionInternal", "trackConversionInternal$library_debug", "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Double;Ljava/lang/String;Ljava/lang/String;)Lio/reactivex/Completable;", "trackInputFocus", "trackInputFocusInternal", "trackInputFocusInternal$library_debug", "trackPurchase", "customerIds", "", "orderID", "([Ljava/lang/String;Ljava/lang/Double;Ljava/lang/String;Ljava/lang/String;)V", "trackPurchaseInternal", "trackPurchaseInternal$library_debug", "([Ljava/lang/String;Ljava/lang/Double;Ljava/lang/String;Ljava/lang/String;)Lio/reactivex/Completable;", "trackSearchResultClick", "trackSearchResultClickInternal", "trackSearchResultClickInternal$library_debug", "trackSearchResultsLoaded", "trackSearchResultsLoadedInternal", "trackSearchResultsLoadedInternal$library_debug", "trackSearchSubmit", "trackSearchSubmitInternal", "trackSearchSubmitInternal$library_debug", "trackSessionStart", "trackSessionStartInternal", "trackSessionStartInternal$library_debug", "library_debug"})
public final class ConstructorIo {
    private static io.constructor.data.DataManager dataManager;
    private static io.constructor.data.local.PreferencesHelper preferenceHelper;
    private static io.constructor.data.memory.ConfigMemoryHolder configMemoryHolder;
    private static android.content.Context context;
    private static io.reactivex.disposables.CompositeDisposable disposable;
    @org.jetbrains.annotations.NotNull()
    private static final kotlin.Lazy component$delegate = null;
    private static kotlin.jvm.functions.Function1<? super java.lang.String, kotlin.Unit> sessionIncrementHandler;
    public static final io.constructor.core.ConstructorIo INSTANCE = null;
    
    @org.jetbrains.annotations.Nullable()
    public final java.lang.String getUserId() {
        return null;
    }
    
    public final void setUserId(@org.jetbrains.annotations.Nullable()
    java.lang.String value) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.constructor.injection.component.AppComponent getComponent$library_debug() {
        return null;
    }
    
    /**
     * *  Initializes the client
     *     *  @param context the context
     *     *  @param constructorIoConfig the client configuration
     */
    public final void init(@org.jetbrains.annotations.Nullable()
    android.content.Context context, @org.jetbrains.annotations.NotNull()
    io.constructor.core.ConstructorIoConfig constructorIoConfig) {
    }
    
    /**
     * * Returns the current session identifier (an incrementing integer)
     */
    public final int getSessionId() {
        return 0;
    }
    
    /**
     * * Returns the current client identifier (a random GUID assigned to the app running on the device)
     */
    @org.jetbrains.annotations.NotNull()
    public final java.lang.String getClientId() {
        return null;
    }
    
    public final void testInit$library_debug(@org.jetbrains.annotations.Nullable()
    android.content.Context context, @org.jetbrains.annotations.NotNull()
    io.constructor.core.ConstructorIoConfig constructorIoConfig, @org.jetbrains.annotations.NotNull()
    io.constructor.data.DataManager dataManager, @org.jetbrains.annotations.NotNull()
    io.constructor.data.local.PreferencesHelper preferenceHelper, @org.jetbrains.annotations.NotNull()
    io.constructor.data.memory.ConfigMemoryHolder configMemoryHolder) {
    }
    
    public final void appMovedToForeground() {
    }
    
    /**
     * * Returns a list of autocomplete suggestions
     */
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Observable<io.constructor.data.ConstructorData<io.constructor.data.model.autocomplete.AutocompleteResponse>> getAutocompleteResults(@org.jetbrains.annotations.NotNull()
    java.lang.String term) {
        return null;
    }
    
    /**
     * * Returns a list of search results including filters, categories, sort options, etc.
     *     * @param term the term to search for
     *     * @param facets  additional facets used to refine results
     *     * @param page the page number of the results
     *     * @param perPage The number of results per page to return
     *     * @param groupId category facet used to refine results
     *     * @param sortBy the sort method for results
     *     * @param sortOrder the sort order for results
     */
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Observable<io.constructor.data.ConstructorData<io.constructor.data.model.search.SearchResponse>> getSearchResults(@org.jetbrains.annotations.NotNull()
    java.lang.String term, @org.jetbrains.annotations.Nullable()
    java.util.List<? extends kotlin.Pair<java.lang.String, ? extends java.util.List<java.lang.String>>> facets, @org.jetbrains.annotations.Nullable()
    java.lang.Integer page, @org.jetbrains.annotations.Nullable()
    java.lang.Integer perPage, @org.jetbrains.annotations.Nullable()
    java.lang.Integer groupId, @org.jetbrains.annotations.Nullable()
    java.lang.String sortBy, @org.jetbrains.annotations.Nullable()
    java.lang.String sortOrder) {
        return null;
    }
    
    /**
     * * Returns a list of browse results including filters, categories, sort options, etc.
     *     * @param filterName filter name to display results from
     *     * @param filterValue filter value to display results from
     *     * @param facets  additional facets used to refine results
     *     * @param page the page number of the results
     *     * @param perPage The number of results per page to return
     *     * @param groupId category facet used to refine results
     *     * @param sortBy the sort method for results
     *     * @param sortOrder the sort order for results
     */
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Observable<io.constructor.data.ConstructorData<io.constructor.data.model.browse.BrowseResponse>> getBrowseResults(@org.jetbrains.annotations.NotNull()
    java.lang.String filterName, @org.jetbrains.annotations.NotNull()
    java.lang.String filterValue, @org.jetbrains.annotations.Nullable()
    java.util.List<? extends kotlin.Pair<java.lang.String, ? extends java.util.List<java.lang.String>>> facets, @org.jetbrains.annotations.Nullable()
    java.lang.Integer page, @org.jetbrains.annotations.Nullable()
    java.lang.Integer perPage, @org.jetbrains.annotations.Nullable()
    java.lang.Integer groupId, @org.jetbrains.annotations.Nullable()
    java.lang.String sortBy, @org.jetbrains.annotations.Nullable()
    java.lang.String sortOrder) {
        return null;
    }
    
    /**
     * * Tracks session start events
     */
    private final void trackSessionStart() {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackSessionStartInternal$library_debug() {
        return null;
    }
    
    /**
     * * Tracks input focus events
     *     * @param term the term currently in the search bar
     */
    public final void trackInputFocus(@org.jetbrains.annotations.Nullable()
    java.lang.String term) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackInputFocusInternal$library_debug(@org.jetbrains.annotations.Nullable()
    java.lang.String term) {
        return null;
    }
    
    /**
     * * Tracks autocomplete select events
     *     * @param searchTerm the term selected, i.e. "Pumpkin"
     *     * @param originalQuery the term in the search bar, i.e. "Pum"
     *     * @param sectionName the section the selection came from, i.e. "Search Suggestions"
     *     * @param resultGroup the group to search within if a user selected to search in a group, i.e. "Pumpkin in Canned Goods"
     *     * @param resultID the result ID of the autocomplete response that the selection came from
     */
    public final void trackAutocompleteSelect(@org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.NotNull()
    java.lang.String originalQuery, @org.jetbrains.annotations.NotNull()
    java.lang.String sectionName, @org.jetbrains.annotations.Nullable()
    io.constructor.data.model.common.ResultGroup resultGroup, @org.jetbrains.annotations.Nullable()
    java.lang.String resultID) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackAutocompleteSelectInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.NotNull()
    java.lang.String originalQuery, @org.jetbrains.annotations.NotNull()
    java.lang.String sectionName, @org.jetbrains.annotations.Nullable()
    io.constructor.data.model.common.ResultGroup resultGroup, @org.jetbrains.annotations.Nullable()
    java.lang.String resultID) {
        return null;
    }
    
    /**
     * * Tracks search submit events
     *     * @param searchTerm the term selected, i.e. "Pumpkin"
     *     * @param originalQuery the term in the search bar, i.e. "Pum"
     *     * @param resultGroup the group to search within if a user elected to search in a group, i.e. "Pumpkin in Canned Goods"
     */
    public final void trackSearchSubmit(@org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.NotNull()
    java.lang.String originalQuery, @org.jetbrains.annotations.Nullable()
    io.constructor.data.model.common.ResultGroup resultGroup) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackSearchSubmitInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.NotNull()
    java.lang.String originalQuery, @org.jetbrains.annotations.Nullable()
    io.constructor.data.model.common.ResultGroup resultGroup) {
        return null;
    }
    
    /**
     * * Tracks search results loaded (a.k.a. search results viewed) events
     *     * @param term the term that results are displayed for, i.e. "Pumpkin"
     *     * @param resultCount the number of results for that term
     */
    public final void trackSearchResultsLoaded(@org.jetbrains.annotations.NotNull()
    java.lang.String term, int resultCount) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackSearchResultsLoadedInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String term, int resultCount) {
        return null;
    }
    
    /**
     * * Tracks search result click events
     *     * @param itemName the name of the clicked item i.e. "Kabocha Pumpkin"
     *     * @param customerId the identifier of the clicked item i.e "PUMP-KAB-0002"
     *     * @param searchTerm the term that results are displayed for, i.e. "Pumpkin"
     *     * @param sectionName the section that the results came from, i.e. "Products"
     *     * @param resultID the result ID of the search response that the click came from
     */
    public final void trackSearchResultClick(@org.jetbrains.annotations.NotNull()
    java.lang.String itemName, @org.jetbrains.annotations.NotNull()
    java.lang.String customerId, @org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName, @org.jetbrains.annotations.Nullable()
    java.lang.String resultID) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackSearchResultClickInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String itemName, @org.jetbrains.annotations.NotNull()
    java.lang.String customerId, @org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName, @org.jetbrains.annotations.Nullable()
    java.lang.String resultID) {
        return null;
    }
    
    /**
     * * Tracks conversion (a.k.a add to cart) events
     *     * @param itemName the name of the converting item i.e. "Kabocha Pumpkin"
     *     * @param customerId the identifier of the converting item i.e "PUMP-KAB-0002"
     *     * @param searchTerm the search term that lead to the event (if adding to cart in a search flow)
     *     * @param sectionName the section that the results came from, i.e. "Products"
     */
    public final void trackConversion(@org.jetbrains.annotations.NotNull()
    java.lang.String itemName, @org.jetbrains.annotations.NotNull()
    java.lang.String customerId, @org.jetbrains.annotations.Nullable()
    java.lang.Double revenue, @org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackConversionInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String itemName, @org.jetbrains.annotations.NotNull()
    java.lang.String customerId, @org.jetbrains.annotations.Nullable()
    java.lang.Double revenue, @org.jetbrains.annotations.NotNull()
    java.lang.String searchTerm, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName) {
        return null;
    }
    
    /**
     * * Tracks purchase events
     *     * @param customerIds the identifiers of the purchased items
     *     * @param revenue the revenue of the purchase event
     *     * @param orderID the identifier of the order
     */
    public final void trackPurchase(@org.jetbrains.annotations.NotNull()
    java.lang.String[] customerIds, @org.jetbrains.annotations.Nullable()
    java.lang.Double revenue, @org.jetbrains.annotations.NotNull()
    java.lang.String orderID, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackPurchaseInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String[] customerIds, @org.jetbrains.annotations.Nullable()
    java.lang.Double revenue, @org.jetbrains.annotations.NotNull()
    java.lang.String orderID, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName) {
        return null;
    }
    
    /**
     * * Tracks browse result loaded (a.k.a. browse results viewed) events
     *     * @param filterName the name of the primary filter, i.e. "Aisle"
     *     * @param filterValue the value of the primary filter, i.e. "Produce"
     *     * @param resultCount the number of results for that filter name/value pair
     */
    public final void trackBrowseResultsLoaded(@org.jetbrains.annotations.NotNull()
    java.lang.String filterName, @org.jetbrains.annotations.NotNull()
    java.lang.String filterValue, int resultCount, @org.jetbrains.annotations.NotNull()
    java.lang.String url) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackBrowseResultsLoadedInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String filterName, @org.jetbrains.annotations.NotNull()
    java.lang.String filterValue, int resultCount, @org.jetbrains.annotations.NotNull()
    java.lang.String url) {
        return null;
    }
    
    /**
     * * Tracks browse result click events
     *     * @param filterName the name of the primary filter, i.e. "Aisle"
     *     * @param filterValue the value of the primary filter, i.e. "Produce"
     *     * @param customerId the item identifier of the clicked item i.e "PUMP-KAB-0002"
     *     * @param sectionName the section that the results came from, i.e. "Products"
     *     * @param resultID the result ID of the browse response that the selection came from
     */
    public final void trackBrowseResultClick(@org.jetbrains.annotations.NotNull()
    java.lang.String filterName, @org.jetbrains.annotations.NotNull()
    java.lang.String filterValue, @org.jetbrains.annotations.NotNull()
    java.lang.String customerId, int resultPositionOnPage, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName, @org.jetbrains.annotations.Nullable()
    java.lang.String resultID) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final io.reactivex.Completable trackBrowseResultClickInternal$library_debug(@org.jetbrains.annotations.NotNull()
    java.lang.String filterName, @org.jetbrains.annotations.NotNull()
    java.lang.String filterValue, @org.jetbrains.annotations.NotNull()
    java.lang.String customerId, int resultPositionOnPage, @org.jetbrains.annotations.Nullable()
    java.lang.String sectionName, @org.jetbrains.annotations.Nullable()
    java.lang.String resultID) {
        return null;
    }
    
    private ConstructorIo() {
        super();
    }
}