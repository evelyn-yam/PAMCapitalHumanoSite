window.blazorMetronic = {
    init() {
        window.requestAnimationFrame(() => {
            this.runInitializers();
        });
    },

    cleanup() {
        // Reserved for page-level cleanup when a component requires it.
    },

    runInitializers() {
        console.log("[inicializando Metronic components]");

        const initializers = [
            window.KTComponents,

            window.KTMenu,
            window.KTDropdown,
            window.KTDrawer,
            window.KTModal,
            window.KTSticky,
            window.KTScrollspy,
            window.KTScrollable,
            window.KTReparent,

            window.KTAccordion,
            window.KTCollapse,
            window.KTTabs,
            window.KTToggle,
            window.KTToggleGroup,
            window.KTTooltip,
            window.KTDismiss,
            window.KTToast,
            window.KTCarousel,
            window.KTSlider,
            window.KTStepper,

            window.KTSelect,
            window.KTTogglePassword,
            window.KTColorPicker,
            window.KTPinInput,
            window.KTClipboard,

            window.KTDropzone,
            window.KTSortable,

            window.KTDataTable,
            window.KTDataTablesNet,

            window.KTThemeSwitch,
            window.KTThemeMode,

            window.KTTinyMCE,
            window.KTFullCalendar,
            window.KTLeaflet,

            window.KTCanvasConfetti,
            window.KTImageInput,
            window.KTScroll
        ];

        for (const component of initializers) {
            if (!component) continue;

            try {
                if (typeof component.reinit === "function") {
                    component.reinit();
                    continue;
                }

                if (typeof component.init === "function") {
                    component.init();
                    continue;
                }

                if (typeof component.createInstances === "function") {
                    component.createInstances();
                }
            } catch (error) {
                console.warn(
                    "[blazorMetronic] No se pudo inicializar componente Metronic:",
                    component,
                    error
                );
            }
        }
    }
};
