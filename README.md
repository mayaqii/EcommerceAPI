<<<<<<< HEAD
# Ecommerce Web API 🛒

Proste Web API napisane w technologii .NET służące do zarządzania zamówieniami oraz produktami w systemie e-commerce. Projekt korzysta z bazy danych SQLite oraz Entity Framework Core i implementuje relację wiele-do-wielu między produktami a zamówieniami.

---

## Dokumentacja Procesu CI/CD (Automatyzacja)

W projekcie skonfigurowano proces ciągłej integracji (**CI - Continuous Integration**) za pomocą narzędzia **GitHub Actions**.

### Jak to działa?
Każda zmiana w kodzie (komenda `git push`) wysłana na główną gałąź (`main`) automatycznie uruchamia proces testowy na serwerach GitHub. Dzięki temu mamy pewność, że nowo dodany kod nie "psuje" aplikacji i poprawnie się kompiluje.

### Kroki wykonywane przez serwer:
1. **Wyzwalacz (Trigger):** Wykrycie zdarzenia `push` na gałęzi `main`.
2. **Środowisko:** Uruchomienie czystej maszyny wirtualnej z systemem Linux (Ubuntu).
3. **Pobranie kodu:** Klonowanie repozytorium do pamięci maszyny wirtualnej (`actions/checkout`).
4. **Instalacja SDK:** Przygotowanie środowiska uruchomieniowego .NET.
5. **Przywracanie paczek (`dotnet restore`):** Pobranie wszystkich bibliotek zewnętrznych potrzebnych do działania aplikacji.
6. **Budowanie (`dotnet build`):** Kompilacja projektu w konfiguracji produkcyjnej (`Release`). Jeśli w kodzie pojawią się błędy składniowe, ten etap przerwie proces i powiadomi o błędzie.

### Status Budowania (Badge)
Możesz śledzić status ostatniego uruchomienia workflow bezpośrednio w zakładce **Actions** na swoim profilu GitHub.


## Wdrożenie w Chmurze (Alternatywne środowisko - Render)

Aplikacja została pomyślnie wdrożona i udostępniona publicznie z pominięciem platformy Microsoft Azure.

### Jak się połączyć z wdrożoną aplikacją?
Główny adres URL wdrożonej aplikacji:  
`https://moje-ecommerce-api.onrender.com`

Dokumentacja interfejsu API (Swagger):  
👉 `https://moje-ecommerce-api.onrender.com/swagger/index.html`

### Wykorzystane usługi:
1. **Render Web Services:** Platforma chmurowa typu PaaS (Platform as a Service), która automatycznie uruchamia aplikację na bazie dostarczonego pliku `Dockerfile`. Obsługuje ona darmowy proces hostingu i automatycznie skaluje aplikację do zera w przypadku braku ruchu (co oszczędza zasoby).

### Informacje konfiguracyjne:
* **Konteneryzacja:** Aplikacja została w pełni skonteneryzowana przy użyciu **Dockera**, co uniezależnia ją od konkretnego dostawcy chmurowego (kod zadziała tak samo na Render, AWS czy lokalnym serwerze).
* **Automatyczne wdrożenie (CD):** Pipeline w GitHub Actions korzysta z mechanizmu **Deploy Hooks**. Po każdym udanym teście i zbudowaniu kodu na gałęzi `main`, GitHub wysyła powiadomienie do platformy Render, która w bezpieczny sposób aktualizuje działającą aplikację bez przestojów.

---

## Infrastruktura jako Kod (IaaC) przy użyciu Bicep

W projekcie zaimplementowano podejście **Infrastruktury jako Kod (Infrastructure as Code)** przy użyciu języka **Bicep**. Pozwala on na deklaratywne definiowanie zasobów chmurowych bezpośrednio wewnątrz kodu źródłowego.

### Zdefiniowane zasoby (w pliku `Infrastructure/main.bicep`):
1. **App Service Plan (`Microsoft.Web/serverfarms`):** Definicja darmowego serwera wirtualnego w systemie Linux (warstwa F1).
2. **Web App (`Microsoft.Web/sites`):** Definicja samej aplikacji webowej skonfigurowanej pod uruchamianie środowiska `.NET 8`.

### Uruchomienie z poziomu Pipeline'a:
Proces wdrożenia i walidacji kodu infrastruktury został w pełni zautomatyzowany w potoku **GitHub Actions** (`.github/workflows/dotnet-ci.yml`).

Z uwagi na konfigurację środowiska testowego (brak aktywnej subskrypcji komercyjnej), pipeline realizuje proces **Continuous Integration dla IaaC**:
1. Serwer automatycznie pobiera oficjalne narzędzie kompilujące **Bicep CLI**.
2. Uruchamiana jest komenda `bicep build Infrastructure/main.bicep`.
3. Komenda ta sprawdza poprawność typów danych, składnię, zależności oraz relacje między zasobami, generując finalny szablon transpilacji ARM. Jeśli w definicji infrastruktury pojawiłby się błąd, pipeline natychmiast zostanie przerwany.
=======
# EcommerceAPI
>>>>>>> 0fc83f0bbd9f6f2d660fbdb5ebcb26a112731400
