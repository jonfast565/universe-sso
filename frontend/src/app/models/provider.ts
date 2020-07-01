class ProviderViewModel {
    public name: string
    public background: string
    public logo: string
    public selected: boolean = false;
}

class ProviderViewModelSlim {
    public name: string
    public logo: string
    public selected: boolean = false;
}

export {
    ProviderViewModel,
    ProviderViewModelSlim
}