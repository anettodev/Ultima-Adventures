# Build Platform Comparison

Guide for building Ultima Adventures on different platforms.

---

## Current Setup

**Target Framework:** `.NET Framework 4.8` (`net48`)

**Branches:** `main` (production) and `dev` (development)

**Current CI/CD:** Windows runner with MSBuild ✅

---

## Platform Options

### Option 1: Windows with MSBuild (RECOMMENDED) ✅

**Current workflow:** `.github/workflows/build-and-release.yml`

**Build command:**
```powershell
msbuild Scripts/Scripts.csproj `
  /p:Configuration=Release `
  /p:Platform=AnyCPU `
  /p:OutputPath=../bin/Release/
```

**Pros:**
- ✅ **Native .NET Framework 4.8** - Official Microsoft support
- ✅ **100% compatibility** - Guaranteed to work
- ✅ **Best for .NET Framework** - Purpose-built
- ✅ **Well-tested** - Standard build process
- ✅ **No surprises** - What you build is what you get

**Cons:**
- ⏱️ **Slightly slower** - Windows runners take ~30 seconds longer to start
- 💰 **Cost** - Windows runners use 2x minutes (only matters for private repos)

**Build time:** ~3-5 minutes total

**Recommendation:** ⭐⭐⭐⭐⭐ **Use this** (already configured)

---

### Option 2: Linux with Mono ⚠️

**Experimental workflow:** `.github/workflows/build-linux-mono.yml`

**Build command:**
```bash
msbuild Scripts/Scripts.csproj \
  /p:Configuration=Release \
  /p:Platform=AnyCPU \
  /p:OutputPath=../bin/Release/
```

**Pros:**
- ⚡ **Faster startup** - Linux runners start quicker
- 💰 **Cost** - Uses 1x minutes (vs 2x for Windows)
- 🐧 **Linux-native** - If you prefer Linux

**Cons:**
- ⚠️ **Mono compatibility** - Not 100% compatible with .NET Framework 4.8
- ⚠️ **Potential issues** - May have subtle compilation differences
- ⚠️ **Less tested** - Not the standard path for .NET Framework
- ⚠️ **Debugging harder** - Mono-specific issues can be tricky

**Build time:** ~2-4 minutes total

**Recommendation:** ⭐⭐⚠️⚠️⚠️ **Only if Windows builds fail or are too slow**

---

### Option 3: Migrate to .NET 6+ (Future)

**Would require:** Updating `Scripts.csproj` to `net6.0` or `net8.0`

**Benefits:**
- ✅ **Cross-platform** - Build on Windows, Linux, or macOS
- ✅ **Modern** - Latest .NET features
- ✅ **Better performance** - .NET 6+ is faster
- ✅ **Long-term support** - .NET Framework 4.8 is legacy

**Challenges:**
- 🔧 **Migration effort** - Would need to update code
- 🔧 **Breaking changes** - Some APIs changed
- 🔧 **Testing** - Everything needs retesting
- 🔧 **Dependencies** - OrbServerSDK.dll may need updates

**Recommendation:** ⭐⭐⭐⭐⚠️ **Consider for long-term, but not urgent**

---

## Current Configuration Summary

Your CI/CD is configured to use:

### Branches
- **`main`** - Production releases (protected)
- **`dev`** - Active development

### Build Platform
- **Windows runner** with MSBuild
- **.NET Framework 4.8**
- Runs on: `runs-on: windows-latest`

### Workflows

| Workflow | Platform | When | Purpose |
|----------|----------|------|---------|
| `build-and-release.yml` | Windows | Push to main/dev, tags | **Main build** ✅ |
| `build-pr.yml` | Windows | Pull requests | PR validation |
| `nightly-build.yml` | Windows | Daily 2AM UTC | Nightly builds |
| `build-linux-mono.yml` | Linux | Manual only | **Experimental** ⚠️ |

---

## Why Windows is Recommended

### .NET Framework 4.8 is Windows-Only

From Microsoft:
> .NET Framework is a Windows-only implementation of .NET. It supports building any type of app that runs on Windows.

Your project:
```xml
<TargetFramework>net48</TargetFramework>
```

This targets **.NET Framework 4.8**, which is:
- Windows-specific
- Best built with MSBuild on Windows
- Not fully compatible with Mono

### Mono Limitations

Mono is an open-source implementation of .NET Framework, but:
- ⚠️ **Not 100% compatible** - Some APIs behave differently
- ⚠️ **Edge cases** - Subtle differences in compilation
- ⚠️ **Less support** - Smaller community for .NET Framework on Mono

### GitHub Actions Pricing

**For public repositories:** ✅ **Free unlimited builds** (both Windows and Linux)

**For private repositories:**
- Linux: 1x minutes (2,000 free/month)
- Windows: 2x minutes (2,000 free/month = 1,000 Windows minutes)

Example: 10 builds/day on Windows = ~300 minutes/month (well within free tier)

---

## Testing Mono Builds (Optional)

If you want to try Linux/Mono builds:

### 1. Test Manually

```bash
# On a Linux machine with Mono installed
sudo apt-get install -y mono-complete msbuild nuget

# Clone repo
git clone https://github.com/YOUR_ORG/Ultima-Adventures.git
cd Ultima-Adventures

# Restore packages
nuget restore Scripts/Scripts.csproj

# Build
msbuild Scripts/Scripts.csproj \
  /p:Configuration=Release \
  /p:Platform=AnyCPU \
  /p:OutputPath=../bin/Release/

# Verify
ls -lh bin/Release/Scripts.dll
```

### 2. Compare Builds

```bash
# Build on Windows
# Download Scripts.dll → Scripts-windows.dll

# Build on Linux with Mono
# Download Scripts.dll → Scripts-mono.dll

# Compare file sizes
ls -lh Scripts-*.dll

# Compare with a diff tool (they will differ, but should be similar size)
```

### 3. Test in Production

```bash
# Deploy Mono build to a test server
# Test thoroughly:
# - Server starts?
# - All systems load?
# - No runtime errors?
# - Players can connect?
# - Combat works?
# - Magic works?
# - No crashes?
```

### 4. If Mono Works

Enable the Linux workflow in `build-linux-mono.yml`:
```yaml
on:
  push:
    branches: [ main, dev ]  # Uncomment these lines
```

---

## Recommendations by Use Case

### 🎯 For Production Releases
**Use Windows builds** (`.github/workflows/build-and-release.yml`)
- Most reliable
- Best compatibility
- Standard practice

### ⚡ For Quick Iterations in Dev
**Could try Mono** if Windows builds are too slow
- Faster startup
- But test thoroughly first

### 🔬 For Experimentation
**Try Mono builds** with the experimental workflow
- Manual trigger only
- Compare with Windows builds
- Test before using in production

### 🚀 For Long-Term
**Consider migrating to .NET 6+**
- Future-proof
- Cross-platform
- Better performance
- But requires migration effort

---

## Current Setup is Optimal ✅

Your current configuration with **Windows runners** is:
- ✅ **Correct** for .NET Framework 4.8
- ✅ **Reliable** and well-tested
- ✅ **Fast enough** (3-5 minutes)
- ✅ **Free** (for public repos)

**No need to change unless you have specific issues.**

---

## When to Consider Linux/Mono

Only switch to Linux/Mono if:
- ❌ Windows builds are consistently failing
- ❌ Build times are unacceptably slow (>10 minutes)
- ❌ Running out of Windows minutes (private repo with high build frequency)
- ❌ Strong preference for Linux-only infrastructure

Otherwise, **stick with Windows builds**. ✅

---

## Migration Path (Future)

If you want to support true cross-platform builds:

### Phase 1: Test .NET 6+ Compatibility
1. Create test branch
2. Update Scripts.csproj: `<TargetFramework>net6.0</TargetFramework>`
3. Fix compilation errors
4. Test thoroughly

### Phase 2: Multi-Target
```xml
<TargetFrameworks>net48;net6.0</TargetFrameworks>
```
Support both for transition period

### Phase 3: Migrate Fully
Remove `net48`, use only `net6.0` or `net8.0`

**Benefits:**
- Build on any platform (Windows, Linux, macOS)
- Modern .NET features
- Better performance
- Long-term support

**Effort:** Medium to high (depends on code complexity)

---

## Summary

| Platform | Speed | Compatibility | Cost | Recommendation |
|----------|-------|---------------|------|----------------|
| **Windows + MSBuild** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | Free | **Use this** ✅ |
| **Linux + Mono** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⚠️⚠️ | Free | Test first ⚠️ |
| **.NET 6+ (future)** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | Free | Long-term ⭐ |

**Current configuration is correct - no changes needed!** ✅

---

## Questions?

- **Q: Must I use Windows?**
  - A: For .NET Framework 4.8, Windows is most reliable. Mono works but needs testing.

- **Q: Can I test Mono?**
  - A: Yes! Use `.github/workflows/build-linux-mono.yml` (manual trigger)

- **Q: Will Mono builds work in production?**
  - A: Probably, but test thoroughly. Subtle differences may exist.

- **Q: Should I migrate to .NET 6+?**
  - A: Not urgent, but consider for long-term. Enables true cross-platform builds.

- **Q: Why do you have LinuxServer.exe?**
  - A: Your server **runs** on Linux via Mono, but that's different from **building** on Linux. Running on Linux works fine!

- **Q: Is the current setup optimal?**
  - A: Yes! ✅ Windows builds for .NET Framework 4.8 is the standard practice.
