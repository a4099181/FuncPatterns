configuration=debug

csc=$(shell cygpath -u "c:\Program Files (x86)\MSBuild\14.0\Bin\csc.exe")

mspec=$(shell find packages -name mspec-clr4.exe)

nuget=$(shell \
    if [ ! -f packages/nuget.exe ]; then \
        /usr/bin/curl \
            --create-dirs \
            --output packages/nuget.exe \
            --silent \
            https://dist.nuget.org/win-x86-commandline/latest/nuget.exe; \
    fi; \
    \
    /usr/bin/find packages -name 'nuget.exe')

outfile=$(shell basename `pwd` )

packages=$(shell find packages -name '*.dll' -path '*/net45/*' -printf ' %p' )

references=$(shell find packages -name '*.dll' -path '*/net45/*' -printf ' /reference:%p' )

build: nuget-restore create-output-directory build-${configuration}

build-debug: copy-packages ${outfile}-${configuration} test

build-release: ${outfile}-${configuration}

${outfile}-debug: */*/*.cs */*.cs
	"${csc}" /nologo /target:library /debug ${references} /out:bin/${configuration}/${outfile}.dll $?

${outfile}-release: */*/*.cs
	"${csc}" /nologo /target:library /out:bin/${configuration}/${outfile}.dll $?

clean:
	git clean -dxf

copy-packages:
	cp ${packages} bin/${configuration}/

create-output-directory:
	mkdir -p bin/${configuration}

mspec: bin/${configuration}/${outfile}.dll
	"${mspec}" --silent $<

nuget-restore: packages.config
	${nuget} restore $< -PackagesDirectory packages

test: mspec
