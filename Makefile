configuration=debug
csc=$(shell cygpath -u "c:\Program Files (x86)\MSBuild\14.0\Bin\csc.exe")
outfile=$(shell basename `pwd` )

build: create-output-directory build-${configuration}

build-debug: ${outfile}-${configuration}

build-release: ${outfile}-${configuration}

${outfile}-debug: */*/*.cs
	"${csc}" /nologo /target:library /debug /out:bin/${configuration}/${outfile}.dll $?

${outfile}-release: */*/*.cs
	"${csc}" /nologo /target:library /out:bin/${configuration}/${outfile}.dll $?

create-output-directory:
	mkdir -p bin/${configuration}

clean:
	git clean -dxf

